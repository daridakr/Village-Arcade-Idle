using Sirenix.OdinInspector;
using UnityEngine;

namespace ForeverVillage
{
    public abstract class ItemConfig : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField][PreviewField] private Sprite _icon;
        [SerializeField] private int _stackCapacity = 1;

        public string Name => _name;
        public string Description => _description;
        public Sprite Icon => _icon;
        public int StackCapacity => _stackCapacity;

        private void OnValidate()
        {
            if (_stackCapacity <= 0)
                _stackCapacity = 1;
        }

        public abstract Item InstantiateItem();
    }
}