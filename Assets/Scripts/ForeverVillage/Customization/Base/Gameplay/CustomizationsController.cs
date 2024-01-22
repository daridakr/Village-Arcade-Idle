using System;
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
        private Customization _currentCustomization;
        private int _startIndex = 0;

        public event Action Initialized;

        public void SetupCustomizationsFor(MonoBehaviour monoCustomizable)
        {
            _customizations = new List<Customization>();

            CustomizationConfig[] configs = _catalog.GetAllCustomizations();

            foreach (var config in configs)
            {
                var customization = config.InstantiateCustomization(monoCustomizable);
                _customizations.Add(customization);
            }

            UpdateCustoms();
            Initialized?.Invoke();
        }

        private void UpdateCustoms()
        {
            foreach (var customization in _customizations)
            {
                customization.ApplyCustom(_startIndex);
            }
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
    }
}