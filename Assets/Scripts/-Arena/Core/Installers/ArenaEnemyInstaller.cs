using UnityEngine;
using Zenject;

namespace Arena
{
    public sealed class ArenaEnemyInstaller : MonoInstaller
    {
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private EnemyHealthConfig _healthConfig;

        public override void InstallBindings()
        {
            BindFactory();
            BindSpawner();
        }

        private void BindFactory()
        {
            Container.Bind<IEnemyFactory>().To<EnemyFactory>().AsSingle();
        }

        private void BindSpawner()
        {
            Container.Bind<EnemySpawner>().FromInstance(_enemySpawner).AsSingle();
        }
    }
}

