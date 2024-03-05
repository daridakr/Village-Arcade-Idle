using UnityEngine;
using Zenject;

namespace ForeverVillage
{
    public class ItemsGiver : MonoBehaviour
    {
        [SerializeField] private ItemsCatalog _catalog;

        private PlayerInventory _inventory;

        [Inject]
        private void Construct(PlayerInventory playerInventory)
        {
            _inventory = playerInventory;
        }

        private void Start()
        {
            ItemConfig[] itemConfigs = _catalog.GetData();

            foreach (var config in itemConfigs)
            {
                var item = config.InstantiateItem();
                _inventory.PutItem(item, 20);
            }
        }
    }
}