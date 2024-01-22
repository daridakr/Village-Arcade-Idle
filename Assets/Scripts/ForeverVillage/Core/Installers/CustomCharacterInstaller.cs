using UnityEngine;
using Zenject;

namespace ForeverVillage.Scripts.Character
{
    public class CustomCharacterInstaller : MonoInstaller
    {
        [SerializeField] private GenderButtonsViewer _genderPresenter;
        [SerializeField] private SpecializationsController _specializations;
        [SerializeField] private CustomizationsController _customizations;

        public override void InstallBindings()
        {
            BindFactory();
            BindControllers();
            BindPresenters();
        }

        private void BindFactory()
        {
            Container.Bind<ICustomizableCharacterFactory>().To<CustomizableCharacterFactory>().AsSingle();
            Container.Bind<CustomizableCharacter>().AsSingle();
        }

        private void BindControllers()
        {
            Container.Bind<ISpecializationsController>().
                To<SpecializationsController>().FromInstance(_specializations).AsSingle();

            Container.Bind<ICustomizationsController>().
               To<CustomizationsController>().FromInstance(_customizations).AsSingle();
        }

        private void BindPresenters()
        {
            Container.Bind<GenderButtonsViewer>().FromInstance(_genderPresenter).AsSingle();
        }
    }
}