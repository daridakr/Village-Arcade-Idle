using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace ForeverVillage.Scripts
{
    public sealed class CustomizationsController : MonoBehaviour,
        ICustomizationsController
    {
        [SerializeField][FormerlySerializedAs("assets")] private CustomizationCatalog _catalog;

        private Dictionary<string, Customization> _customizations;
        private Customization _currentCustomization;

        public event Action Initialized;

        public void SetupCustomizationsFor(MonoBehaviour monoCustomizable)
        {
            _customizations = new Dictionary<string, Customization>();

            CustomizationConfig[] configs = _catalog.GetAllCustomizations();

            foreach (var config in configs)
            {
                var customization = config.InstantiateCustomization(monoCustomizable);
                _customizations.Add(customization.Id, customization);
            }

            UpdateCustoms();
            Initialized?.Invoke();
        }

        public void SelectCustom(ICustomization customization)
        {
            _currentCustomization = (Customization)customization;
            _currentCustomization.ApplyCustom();
        }

        public void NextCurrent()
        {
            _currentCustomization?.Next();
        }

        public void PreviousCurrent()
        {
            _currentCustomization?.Previous();
        }

        public ICustomization GetCustomization(string guid)
        {
            return _customizations[guid];
        }

        public ICustomization[] GetAllCustomizations()
        {
            if (_customizations == null)
                return null;

            return _customizations.Values.ToArray();
        }

        public void UpdateCustoms()
        {
            foreach (var customization in _customizations.Values)
            {
                customization.ApplyCustom();
            }
        }
    }
}