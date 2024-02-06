using System;
using System.Collections;
using UnityEngine;

namespace Village
{
    [RequireComponent(typeof(GuidableObject))]
    public class RegionZone : MonoBehaviour
    {
        [SerializeField] private PlayerWalletTrigger _playerWalletTrigger;
        [SerializeField] private ReachableRegion _region;

        private GuidableObject _guidable;
        private RegionZoneData _data;
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
            _data = new RegionZoneData(price, _guidable.GUID);
            _data.CostUpdated += OnCostUpdated;
            _data.Unlocked += OnZonePaid;
            _data.Load();
        }

        private void OnCostUpdated(int cost)
        {
            Updated?.Invoke(cost);
            _data.Save();
        }

        private void LockBy(int condition)
        {
            Locked?.Invoke(condition);
            _region.Unreached -= LockBy;
            _playerWalletTrigger.Disable();
        }

        private void UnlockToBuy()
        {
            _region.Reached -= UnlockToBuy;
            Unlocked?.Invoke();

            _playerWalletTrigger.Enable();
            _playerWalletTrigger.Enter += TriggerEnter;
            _playerWalletTrigger.Exit += TriggerExit;
        }

        private void TriggerEnter(PlayerWallet wallet)
        {
            if (_tryBuy != null)
                StopCoroutine(_tryBuy);

            _tryBuy = StartCoroutine(TryBuyRegion(wallet));
        }

        private void TriggerExit(PlayerWallet wallet)
        {
            StopCoroutine(_tryBuy);
        }

        private IEnumerator TryBuyRegion(PlayerWallet wallet)
        {
            yield return null;

            bool delayed = false;

            var playerMovement = wallet.GetComponent<PlayerMovementUpgradable>();

            while (true)
            {
                if (playerMovement.IsMoving == false)
                {
                    if (delayed == false)
                        yield return new WaitForSeconds(0.75f);

                    BuyRegion(wallet);
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

        private void BuyRegion(PlayerWallet wallet)
        {
            if (wallet.IsCoinsEmpty)
                return;

            _reduceValue = Mathf.Clamp((int)(_data.TotalCost * 1.5f * Time.deltaTime), 1, _data.TotalCost);
            if (_data.CurrentCost < _reduceValue)
            {
                _reduceValue = _data.CurrentCost;
            }

            _reduceValue = Mathf.Clamp(_reduceValue, 1, wallet.Coins);

            _data.ReduceCost(_reduceValue);
            wallet.SpendCoins(_reduceValue);

            Triggering?.Invoke();
        }

        private void OnZonePaid()
        {
            _data.CostUpdated -= OnCostUpdated;
            _data.Unlocked -= OnZonePaid;

            Paid?.Invoke();
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            _playerWalletTrigger.Disable();
            _playerWalletTrigger.Enter -= TriggerEnter;
            _playerWalletTrigger.Exit -= TriggerExit;
        }
    }
}