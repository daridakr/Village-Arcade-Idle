using UnityEngine;
using Zenject;

namespace Arena
{
    public class SpellsInstaller : MonoInstaller
    {
        [SerializeField] private SpellsController _spellsController;

        public override void InstallBindings()
        {
            BindSpellsSystem();
        }

        private void BindSpellsSystem()
        {
            //Container.Bind<ITickable>().To<Spell>().AsTransient();

            Container.BindInterfacesAndSelfTo<SpellsController>().FromInstance(_spellsController).AsSingle();
        }
    }
}
