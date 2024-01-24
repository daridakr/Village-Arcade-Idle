using UnityEngine;
using Zenject;

namespace ForeverVillage.Scripts
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Player _prefab;

        private Player _playerInstance;

        public override void InstallBindings()
        {
            SpawnAndBindPlayer();

            BindComponents();
            BindDisplayData();
            BindRepository();
        }

        private void SpawnAndBindPlayer()
        {
            _playerInstance = Container.InstantiatePrefabForComponent<Player>
                (_prefab.gameObject, _prefab.Position, Quaternion.identity, null);

            Container.Bind<Player>().FromInstance(_playerInstance).AsSingle().NonLazy();
        }

        private void BindComponents()
        {
            Container.Bind<PlayerCharacterModel>().FromInstance(_playerInstance.Model).AsSingle();
            Container.Bind<PlayerName>().FromInstance(_playerInstance.Name).AsSingle();
            Container.Bind<PlayerMovement>().FromInstance(_playerInstance.Movement).AsSingle();
            Container.Bind<PlayerTimerCleaner>().FromInstance(_playerInstance.Cleaner).AsSingle();
            Container.Bind<PlayerTimerBuilder>().FromInstance(_playerInstance.Builder).AsSingle();
            Container.Bind<PlayerWallet>().FromInstance(_playerInstance.Wallet).AsSingle();
            Container.Bind<PlayerBuildingsList>().FromInstance(_playerInstance.Buildings).AsSingle();
            Container.Bind<PlayerVillagersList>().FromInstance(_playerInstance.Villagers).AsSingle();
            Container.Bind<PlayerRegionsList>().FromInstance(_playerInstance.Regions).AsSingle();
        }

        private void BindDisplayData()
        {
            Container.BindInterfacesAndSelfTo<PlayerLevel>().FromInstance(_playerInstance.Level).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerCoins>().FromInstance(_playerInstance.Coins).AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerGems>().FromInstance(_playerInstance.Gems).AsSingle();
        }

        private void BindRepository()
        {
            Container.Bind<PlayerPointRepository>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerPointSaver>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerPointSetter>().AsSingle();

            Container.Bind<ISpecializationRepository>().To<SpecializationRepository>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpecializationInstaller>().AsSingle();
        }
    }
}