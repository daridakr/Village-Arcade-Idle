using UnityEngine;

namespace ForeverVillage.Scripts
{
    public abstract class CustomizationConfig : ScriptableObject
    {
        //[SerializeField] private abstract CustomObject[] _customs;
        [SerializeField] private CustomizationMetadata _metadata;

        private Customization _customization;

        public abstract Object[] Customs { get; }
        public CustomizationMetadata Meta => _metadata;
        public Customization Customization => _customization;
        public abstract Customization InstantiateCustomization(MonoBehaviour target);
    }
}