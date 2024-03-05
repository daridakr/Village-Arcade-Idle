using System;

namespace ForeverVillage
{
    public interface IInventoryItemSlot
    {
        public IItemStack Stack { get; }
        public event Action<IInventoryItemSlot> OnEmpty;
    }
}