using System.Collections.Generic;

namespace ForeverVillage
{
    public class Inventory<T>
    {
        private List<T> _items;

        private readonly int _capacity;
        private const int _defaultCapacity = 5;

        public IEnumerable<T> Items => _items;
        public bool IsFree => _items.Count < _capacity;
        //public bool CanAdd(int count) => _items.Count + count <= _capacity;
        public bool IsContains(T item) => _items.Contains(item);

        public Inventory(int capacity = 0)
        {
            _items = new List<T>();

            if (capacity > 0)
                _capacity = capacity;
            else
                _capacity = _defaultCapacity;
        }

        public void Put(T item)
        {
            if (IsFree)
                _items.Add(item);
        }

        public void Remove(T item)
        {
            if (IsContains(item))
                _items.Remove(item);
        }

        public void Clear() => _items.Clear();
    }
}