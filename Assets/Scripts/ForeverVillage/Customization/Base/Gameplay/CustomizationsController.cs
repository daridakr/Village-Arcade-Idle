using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace ForeverVillage.Scripts
{
    public sealed class CustomizationsController : MonoBehaviour,
        ICustomizationsController
    {
        [SerializeField][FormerlySerializedAs("assets")] private CustomizationCatalog _catalog;
        
        private List<Customization> _customizations;
        private int _startIndex = 0;
        private Customization _currentCustomization;

        private void Awake()
        {
            _customizations = new List<Customization>();
        }

        public void SelectCustom(ICustomization customization)
        {
            _currentCustomization = (Customization)customization;
            _currentCustomization.ApplyCustom(_startIndex);
        }

        public ICustomization[] GetAllCustomizations()
        {
            if (_customizations == null)
                return null;

            return _customizations.ToArray();
        }

        public void SetupCustomizationsFor(MonoBehaviour customizable)
        {
            CustomizationConfig[] configs = _catalog.GetAllCustomizations();

            foreach (var config in configs)
            {
                var customization = config.InstantiateCustomization(customizable);
                _customizations.Add(customization);
            }

            UpdateCustoms();
        }

        private void UpdateCustoms()
        {
            foreach (var customization in _customizations)
            {
                customization.ApplyCustom(_startIndex);
            }
        }
    }
}