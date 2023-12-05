using System;
using System.Collections;
using UnityEngine;

public class RegionZone : MonoBehaviour
{
    [SerializeField] private MoneyOwnerTrigger _moneyOwnerTrigger;

    private DynamicCost _cost;
    private Coroutine _tryBuy;
    private int _reduceValue = 1;

    public event Action<int> Locked;
    public event Action<int> PriceUpdated;
    public event Action Buying; // for haptic in future
    public event Action Buyed;

    public void Lock(int required)
    {
        Locked?.Invoke(required);
        _moneyOwnerTrigger.Disable();
    }

    public void Unlock(int price)
    {
        _cost = new DynamicCost(price);
        PriceUpdated?.Invoke(_cost.Current);

        _moneyOwnerTrigger.Enable();
        _moneyOwnerTrigger.Enter += TriggerEnter;
        _moneyOwnerTrigger.Exit += TriggerExit;
    }

    private void TriggerEnter(MoneyOwner moneyOwner)
    {
        if (_tryBuy != null)
            StopCoroutine(_tryBuy);

        _tryBuy = StartCoroutine(TryBuyRegion(moneyOwner));
    }

    private void TriggerExit(MoneyOwner moneyOwner)
    {
        StopCoroutine(_tryBuy);
        //_region.Save();
    }

    private IEnumerator TryBuyRegion(MoneyOwner playerMoney)
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

    private void BuyRegion(MoneyOwner moneyOwner)
    {
        if (moneyOwner.HasMoney == false)
            return;

        _reduceValue = Mathf.Clamp((int)(_cost.Total * 1.5f * Time.deltaTime), 1, _cost.Total);
        if (_cost.Current < _reduceValue)
        {
            _reduceValue = _cost.Current;
        }

        _reduceValue = Mathf.Clamp(_reduceValue, 1, moneyOwner.Balance);

        ReduceCost(_reduceValue);
        moneyOwner.SpendMoney(_reduceValue);

        Buying?.Invoke();
    }

    private void ReduceCost(int value)
    {
        _cost.Subtract(value);
        PriceUpdated?.Invoke(_cost.Current);

        if (_cost.Current == 0)
        {
            Buyed?.Invoke();
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        _moneyOwnerTrigger.Disable();
        _moneyOwnerTrigger.Enter -= TriggerEnter;
        _moneyOwnerTrigger.Exit -= TriggerExit;
    }
}
