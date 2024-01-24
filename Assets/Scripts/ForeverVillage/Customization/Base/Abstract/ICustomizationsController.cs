using System;
using UnityEngine;

namespace ForeverVillage.Scripts
{
    public interface ICustomizationsController
    {
        public void SetupCustomizationsFor(MonoBehaviour monoCustomizable);
        public void SelectCustom(ICustomization customization);
        public void NextCurrent();
        public void PreviousCurrent();
        public void UpdateCustoms();
        public ICustomization GetCustomization(string guid);
        public ICustomization[] GetAllCustomizations();
        public event Action Initialized;
    }
}