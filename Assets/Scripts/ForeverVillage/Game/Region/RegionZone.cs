using System;
using System.Collections;
using UnityEngine;

namespace ForeverVillage.Scripts
{
    [RequireComponent(typeof(GuidableObject))]
    public class RegionZone : MonoBehaviour
    {
        [SerializeField] private PlayerCoinsTrigger _playerCoinsTrigger;
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
            _playerCoinsTrigger.Disable();
        }

        private void UnlockToBuy()
        {
            _region.Reached -= UnlockToBuy;
            Unlocked?.Invoke();

            _playerCoinsTrigger.Enable();
            _playerCoinsTrigger.Enter += TriggerEnter;
            _playerCoinsTrigger.Exit += TriggerExit;
        }

        private void TriggerEnter(PlayerCoins coins)
        {
            if (_tryBuy != null)
                StopCoroutine(_tryBuy);

            _tryBuy = StartCoroutine(TryBuyRegion(coins));
        }

        private void TriggerExit(PlayerCoins coins)
        {
            StopCoroutine(_tryBuy);
        }

        private IEnumerator TryBuyRegion(PlayerCoins playerWithCoins)
        {
            yield return null;

            bool delayed = false;

            var playerMovement = playerWithCoins.GetComponent<PlayerMovement>();

            while (true)
            {
                if (playerMovement.IsMoving == false)
                {
                    if (delayed == false)
                        yield return new WaitForSeconds(0.75f);

                    BuyRegion(playerWithCoins);
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

        private void BuyRegion(PlayerCoins coins)
        {
            if (coins.IsEmpty == false)
                return;

            _reduceValue = Mathf.Clamp((int)(_buyZone.TotalCost * 1.5f * Time.deltaTime), 1, _buyZone.TotalCost);
            if (_buyZone.CurrentCost < _reduceValue)
            {
                _reduceValue = _buyZone.CurrentCost;
            }

            _reduceValue = Mathf.Clamp(_reduceValue, 1, coins.Balance);

            _buyZone.ReduceCost(_reduceValue);
            coins.Spend(_reduceValue);

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
            _playerCoinsTrigger.Disable();
            _playerCoinsTrigger.Enter -= TriggerEnter;
            _playerCoinsTrigger.Exit -= TriggerExit;
        }
    }
}