using ForeverVillage;
using UnityEngine;

namespace Village
{
    public class GemsCollector : MagnitCollector<DroppableGem, CollectableGem>
    {
        [SerializeField] private PlayerGems _playerGems;

        protected override void OnCollected(CollectableGem collectable)
        {
            _playerGems.Recieve(collectable.Value);
        }
    }
}