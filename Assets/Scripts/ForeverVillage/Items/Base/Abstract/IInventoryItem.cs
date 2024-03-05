using UnityEngine;

namespace ForeverVillage
{
    public interface IInventoryItem : IItem
    {
        public string Name { get; }
        public string Description { get; }
        public Sprite Icon { get; }
    }
}