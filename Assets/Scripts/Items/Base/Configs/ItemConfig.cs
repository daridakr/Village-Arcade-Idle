using UnityEngine;

namespace ForeverVillage
{
    public abstract class ItemConfig : ScriptableObject
    {
        [SerializeField] private ItemMetadata _metadata;
        [SerializeField] private int _stackCapacity = 1;

        public string Name => _metadata.Name;
        public string Description => _metadata.Description;
        public Sprite Icon => _metadata.Icon;
        public int StackCapacity => _stackCapacity;

        private void OnValidate()
        {
            if (_stackCapacity <= 0)
                _stackCapacity = 1;
        }

        public abstract Item InstantiateItem();
    }
}