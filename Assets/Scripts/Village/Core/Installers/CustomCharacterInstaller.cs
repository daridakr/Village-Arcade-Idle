using UnityEngine;
using Zenject;

namespace Village.Character
{
    public class CustomCharacterInstaller : MonoInstaller
    {
        [SerializeField] private SpecializationsController _specializations;
        [SerializeField] private CustomizationsController _customizations;

        public override void InstallBindings()
        {
            BindFactory();
            BindControllers();
            BindRepository();
        }

        private void BindFactory()
        {
            Container.Bind<ICustomizableCharacterFactory>().To<CustomizableCharacterFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<CustomizableCharacter>().AsSingle();
        }

        private void BindControllers()
        {
            Container.Bind<ISpecializationsController>().
                To<SpecializationsController>().FromInstance(_specializations).AsSingle();

            Container.Bind<ICustomizationsController>().
               To<CustomizationsController>().FromInstance(_customizations).AsSingle();

            Container.Bind<SpecializationInstantiator>().AsSingle();
        }

        private void BindRepository()
        {
            Container.Bind<ISpecializationRepository>().To<SpecializationRepository>().AsSingle();
            Container.Bind<ICustomizationsRepository>().To<CustomizationsRepository>().AsSingle();
        }
    }
}