using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(GuidableObject))]
public class RegionZone : MonoBehaviour
{
    [SerializeField] private MoneyOwnerTrigger _moneyOwnerTrigger;
    [SerializeField] private ReachableRegion _region;

    private GuidableObject _guidable;
    private BuyZone _buyZone;
    private Coroutine _tryBuy;
    private int _reduceValue = 1;

    public event Action<int> Locked;
    public event Action Unlocked;
    public event Action<int> Updated;
    public event Action Triggering; // for haptic in future
    public event Action Paid;

    private void OnEnable()
    {
        _region.Unreached += LockBy;
        _region.Reached += UnlockToBuy;
    }

    public void InitPrice(int price)
    {
        _guidable = GetComponent<GuidableObject>();
        _buyZone = new BuyZone(price, _guidable.GUID);
        _buyZone.CostUpdated += OnCostUpdated;
        _buyZone.Unlocked += OnZonePaid;
        _buyZone.Load();
    }

    private void OnCostUpdated(int cost)
    {
        Updated?.Invoke(cost);
        _buyZone.Save();
    }

    private void LockBy(int condition)
    {
        Locked?.Invoke(condition);
        _region.Unreached -= LockBy;
        _moneyOwnerTrigger.Disable();
    }

    private void UnlockToBuy()
    {
        _region.Reached -= UnlockToBuy;
        Unlocked?.Invoke();

        _moneyOwnerTrigger.Enable();
        _moneyOwnerTrigger.Enter += TriggerEnter;
        _moneyOwnerTrigger.Exit += TriggerExit;
    }

    private void TriggerEnter(PlayerMoney moneyOwner)
    {
        if (_tryBuy != null)
            StopCoroutine(_tryBuy);

        _tryBuy = StartCoroutine(TryBuyRegion(moneyOwner));
    }

    private void TriggerExit(PlayerMoney moneyOwner)
    {
        StopCoroutine(_tryBuy);
    }

    private IEnumerator TryBuyRegion(PlayerMoney playerMoney)
    {
        yield return null;

        bool delayed = false;

        var playerMovement = playerMoney.GetComponent<PlayerMovement>();

        while (true)
        {
            if (playerMovement.IsMoving == false)
            {
                if (delayed == false)
                    yield return new WaitForSeconds(0.75f);

                BuyRegion(playerMoney);
                //PriceUpdated?.Invoke(_price.Current);
                delayed = true;
            }
            else
            {
                delayed = false;
            }

            yield return null;
        }
    }

    private void BuyRegion(PlayerMoney moneyOwner)
    {
        if (moneyOwner.HasMoney == false)
            return;

        _reduceValue = Mathf.Clamp((int)(_buyZone.TotalCost * 1.5f * Time.deltaTime), 1, _buyZone.TotalCost);
        if (_buyZone.CurrentCost < _reduceValue)
        {
            _reduceValue = _buyZone.CurrentCost;
        }

        _reduceValue = Mathf.Clamp(_reduceValue, 1, moneyOwner.Balance);

        _buyZone.ReduceCost(_reduceValue);
        moneyOwner.Spend(_reduceValue);

        Triggering?.Invoke();
    }

    private void OnZonePaid()
    {
        _buyZone.CostUpdated -= OnCostUpdated;
        _buyZone.Unlocked -= OnZonePaid;

        Paid?.Invoke();
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        _moneyOwnerTrigger.Disable();
        _moneyOwnerTrigger.Enter -= TriggerEnter;
        _moneyOwnerTrigger.Exit -= TriggerExit;
    }
}
