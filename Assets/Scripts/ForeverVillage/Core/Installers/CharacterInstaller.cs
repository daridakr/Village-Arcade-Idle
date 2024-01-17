using UnityEngine;
using Zenject;

namespace ForeverVillage.Scripts.Character
{
    public class CharacterInstaller : MonoInstaller
    {
        [SerializeField] private Transform _point;
        [SerializeField] private GenderPresenter _genderPresenter;

        public override void InstallBindings()
        {
            BindCharacter();
            BindPresenters();
        }

        private void BindCharacter()
        {
            Container.Bind<CharacterCreator>().AsSingle().WithArguments(_point);
        }


        private void BindPresenters()
        {
            Container.Bind<GenderPresenter>().FromInstance(_genderPresenter).AsSingle();
        }
    }
}