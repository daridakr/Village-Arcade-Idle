using UnityEngine;

namespace ForeverVillage.Scripts
{
    public interface ICustomizationsController
    {
        public void SetupCustomizationsFor(MonoBehaviour monoCustomizable);
        public void SelectCustom(ICustomization customization);
        public ICustomization[] GetAllCustomizations();
    }
}