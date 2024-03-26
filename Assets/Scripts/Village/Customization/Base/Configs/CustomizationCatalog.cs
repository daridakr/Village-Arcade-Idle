using UnityEngine;

namespace Village
{
    [CreateAssetMenu(fileName = "NewCustomizationCatalog", menuName = "Customization/Catalog")]
    public sealed class CustomizationCatalog : ScriptableObject
    {
        [SerializeField] private CustomizationConfig[] _data;

        public CustomizationConfig[] GetAllCustomizations()
        {
            return _data;
        }
    }
}