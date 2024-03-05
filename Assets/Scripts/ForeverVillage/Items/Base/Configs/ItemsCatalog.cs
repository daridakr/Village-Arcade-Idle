using UnityEngine;

namespace ForeverVillage
{
    [CreateAssetMenu(fileName = "ItemsCatalog", menuName = "Item/Catalog")]
    public sealed class ItemsCatalog : ScriptableObject
    {
        [SerializeField] private ItemConfig[] _data;

        public ItemConfig[] GetData()
        {
            return _data;
        }
    }
}