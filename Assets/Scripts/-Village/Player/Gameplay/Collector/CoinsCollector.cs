using ForeverVillage;
using UnityEngine;

namespace Arena
{
    public sealed class CoinsCollector : MagnitCollector<DroppableCoin, CollectableCoin>
    {
        [SerializeField] private PlayerCoins _playerCoins;

        protected override void OnCollected(CollectableCoin collectable)
        {
            _playerCoins.Recieve(collectable.Value);
        }
    }
}