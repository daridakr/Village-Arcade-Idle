using UnityEngine;
using Zenject;

namespace Arena
{
    public sealed class ArenaEnemyInstaller : MonoInstaller
    {
        [SerializeField] private EnemyHealthConfig _healthConfig;

        public override void InstallBindings()
        {
            BindFactory();
        }

        private void BindFactory()
        {
            Container.Bind<IEnemyFactory>().To<EnemyFactory>().AsSingle();
        }
    }
}

