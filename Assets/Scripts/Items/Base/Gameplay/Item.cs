using UnityEngine;

namespace ForeverVillage
{
    public abstract class Item :
        IItem
    {
        private readonly ItemConfig _config;

        public string Name => _config.Name;
        public string Description => _config.Description;
        public Sprite Icon => _config.Icon;
        public int StackCapacity => _config.StackCapacity;

        public Item(ItemConfig config)
        {
            _config = config;
        }
    }
}