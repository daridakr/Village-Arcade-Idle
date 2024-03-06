using System;

namespace ForeverVillage
{
    public class ItemStack : 
        IItemStack
    {
        private IItem _item;
        private int _current;
        private int _capacity;

        public IItem Item => _item;
        public int Count => _current;
        public event Action Expended;

        public ItemStack(IItem item, int count = 1)
        {
            _item = item;
            _capacity = _item.StackCapacity;
            _current = count;
        }

        public void Add(int count = 1)
        {
            if (_current + count > _capacity)
                return;

            _current += count;
        }

        public void Expend()
        {
            _current--;

            if (_current <= 0)
            {
                _current = 0;
                Expended?.Invoke();
            }
        }
    }
}