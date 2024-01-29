using UnityEngine;
using Zenject;

namespace Village
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