using Arena;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ForeverVillage
{
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField] private int _capacity;
        [SerializeField] private PlayerEquipment _equipment;

        private Inventory<IInventoryItemSlot> _inventory;

        public IEnumerable<IInventoryItemSlot> Items => _inventory.Items;

        private void Awake()
        {
            _inventory = new Inventory<IInventoryItemSlot>(_capacity);
        }

        public void PutItem(IItem item, int inStack = 1) // need implement to add multiply not stackable items
        {
            if (item == null)
                return;

            bool isStackable = item.StackCapacity > 1;

            if (isStackable)
            {
                IInventoryItemSlot existingSlot = _inventory.Items.FirstOrDefault(slot => slot.Stack.Item == item);

                if (existingSlot != null)
                    PutItemInSlot(existingSlot, ref inStack);

                while (inStack > 0)
                {
                    int count = Mathf.Min(inStack, item.StackCapacity);
                    AddItemSlot(GetNewItemSlot(item, count));
                    inStack -= count;
                }
            }
            else
            {
                AddItemSlot(GetNewItemSlot(item));
            }
        }

        public IItem GetItem(IInventoryItemSlot itemSlot)
        {
            IItem item = null;

            if (itemSlot != null && _inventory.IsContains(itemSlot))
            {
                item = itemSlot.Stack.Item;
                itemSlot.Stack.Expend();
            }

            return item;
        }

        private IInventoryItemSlot GetNewItemSlot(IItem item, int count = 1)
        {
            return new InventoryItemSlot(item, count);
        }

        private void AddItemSlot(IInventoryItemSlot itemSlot)
        {
            if (itemSlot != null && _inventory.IsFree)
            {
                _inventory.Put(itemSlot);
                itemSlot.OnEmpty += RemoveItemSlot;

                Debug.Log(itemSlot.Stack.Item.Name);
                Debug.Log(itemSlot.Stack.Count);
            }
        }

        private void RemoveItemSlot(IInventoryItemSlot itemSlot)
        {
            _inventory.Remove(itemSlot);

            itemSlot.OnEmpty -= RemoveItemSlot;
            itemSlot = null;
        }

        private void PutItemInSlot(IInventoryItemSlot itemSlot, ref int count)
        {
            IItem item = itemSlot.Stack.Item;
            int current = itemSlot.Stack.Count;

            int freeCapacity = item.StackCapacity - current;
            int added = Mathf.Min(count, freeCapacity);

            itemSlot.Stack.Add(added);
            count -= added;
        }
    }
}