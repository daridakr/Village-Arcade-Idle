using System;

namespace ForeverVillage
{
    public class InventoryItemSlot : IInventoryItemSlot
    {
        private IItemStack _itemStack;

        public IItemStack Stack => _itemStack;
        public event Action<IInventoryItemSlot> OnEmpty;

        public InventoryItemSlot(IItem item, int count = 1)
        {
            _itemStack = new ItemStack(item, count);
            _itemStack.Expended += OnItemStackEmptied;
        }

        private void OnItemStackEmptied()
        {
            _itemStack.Expended -= OnItemStackEmptied;
            _itemStack = null;
            OnEmpty?.Invoke(this);
        }
    }
}