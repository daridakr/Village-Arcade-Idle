using UnityEngine;
using Zenject;

namespace ForeverVillage.Scripts.Character
{
    public class CustomCharacterInstaller : MonoInstaller
    {
        [SerializeField] private GenderPresenter _genderPresenter;
        [SerializeField] private SpecializationPresenter _specializationPresenter;

        public override void InstallBindings()
        {
            BindFactory();
            BindPresenters();
        }

        private void BindFactory()
        {
            Container.Bind<ICustomizableCharacterFactory>().To<CustomizableCharacterFactory>().AsSingle();
            Container.Bind<CustomizableCharacter>().AsSingle();
        }

        private void BindPresenters()
        {
            Container.Bind<GenderPresenter>().FromInstance(_genderPresenter).AsSingle();
            Container.Bind<SpecializationPresenter>().FromInstance(_specializationPresenter).AsSingle();
        }
    }
}