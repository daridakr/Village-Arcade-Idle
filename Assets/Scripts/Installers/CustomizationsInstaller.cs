using UnityEngine;
using Zenject;

namespace ForeverVillage.Scripts
{
    public class CustomizationsInstaller : MonoInstaller
    {
        [SerializeField] private CustomizationsController _customizations;

        public override void InstallBindings()
        {
            Container.Bind<ICustomizationsController>().
                To<CustomizationsController>().FromInstance(_customizations).AsSingle();
        }
    }
}