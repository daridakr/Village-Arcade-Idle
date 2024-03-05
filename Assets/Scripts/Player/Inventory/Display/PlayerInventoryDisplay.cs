using UnityEngine;

namespace ForeverVillage
{
    public class PlayerInventoryDisplay : CanvasAnimatedView
    {
        [SerializeField] private InventoryItemView _itemViewPrefab;
        [SerializeField] private Transform _container;

        private PlayerInventory _inventory;

        public override void Display()
        {
            base.Display();

            foreach (IStackableItem item in _inventory.Items)
            {
                InventoryItemView itemView = Instantiate(_itemViewPrefab, _container);
                itemView.Present(item);
            }
        }
    }
}