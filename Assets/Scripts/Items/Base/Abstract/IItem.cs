using UnityEngine;

namespace ForeverVillage
{
    public interface IItem
    {
        public string Name { get; }
        public string Description { get; }
        public Sprite Icon { get; }
        public int StackCapacity { get; }
    }
}