using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace ForeverVillage
{
    public class PlayerInventory : MonoBehaviour
    {
        private Inventory<IInventoryItem> _inventory;

        public IEnumerable<IInventoryItem> Items => _inventory.Items;

        private void Get(IInventoryItem item)
        {
            if (item == null)
                return;

            _inventory.Put(item);
        }
    }

    public class PlayerInventoryDisplay : CanvasAnimatedView
    {
        [SerializeField] private InventoryItemView _itemViewPrefab;
        [SerializeField] private Transform _container;

        private PlayerInventory _inventory;

        public override void Display()
        {
            base.Display();

            foreach (IInventoryItem item in _inventory.Items)
            {
                InventoryItemView itemView = Instantiate(_itemViewPrefab, _container);
                itemView.Present(item);
            }
        }
    }

    public class InventoryItemView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _title;

        public void Present(IInventoryItem item)
        {

        }
    }

    public abstract class Inventory<T>
    {
        private List<T> _items;

        private readonly int _capacity;
        private const int _defaultCapacity = 5;

        public IEnumerable<T> Items => _items;

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
            if (_items.Count + 1 > _capacity)
                return;

            _items.Add(item);
        }

        public void Remove(T item)
        {
            if (_items.Contains(item))
                _items.Remove(item);
        }

        public void Clear() => _items.Clear();
    }
}