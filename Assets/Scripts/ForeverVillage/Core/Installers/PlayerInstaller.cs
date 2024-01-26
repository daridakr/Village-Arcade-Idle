using ForeverVillage.Scripts.Player;
using UnityEngine;
using Zenject;

namespace ForeverVillage.Scripts
{
    public sealed class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerInstanceInfo _prefab;
        [SerializeField] private MovementConfig _movementConfig;
        [SerializeField] private ItemsMagnitConfig _itemsMagnitConfig;

        private PlayerInstanceInfo _playerInstance;

        public override void InstallBindings()
        {
            BindConfigs();
            SpawnAndBindPlayer();
            BindComponents();
            BindDisplayData();
            BindRepository();
        }

        private void BindConfigs()
        {
            Container.Bind<MovementConfig>().FromInstance(_movementConfig).AsSingle().NonLazy();
            Container.Bind<ItemsMagnitConfig>().FromInstance(_itemsMagnitConfig).AsSingle().NonLazy();
        }

        private void SpawnAndBindPlayer()
        {
            _playerInstance = Container.InstantiatePrefabForComponent<PlayerInstanceInfo>
                (_prefab.gameObject, _prefab.Position, Quaternion.identity, null);

            Container.Bind<PlayerInstanceInfo>().FromInstance(_playerInstance).AsSingle().NonLazy();
        }

        private void BindComponents()
        {
            Container.Bind<PlayerMovement>().FromComponentOn(_playerInstance.gameObject).AsSingle();
            Container.Bind<PlayerWallet>().FromComponentOn(_playerInstance.gameObject).AsSingle();
            Container.Bind<PlayerCharacterModel>().FromComponentOn(_playerInstance.Model).AsSingle();
            Container.Bind<PlayerName>().FromComponentOn(_playerInstance.Data).AsSingle();
            Container.Bind<PlayerBuildingsList>().FromComponentOn(_playerInstance.Data).AsSingle();
            Container.Bind<PlayerVillagersList>().FromComponentOn(_playerInstance.Data).AsSingle();
            Container.Bind<PlayerRegionsList>().FromComponentOn(_playerInstance.Data).AsSingle();
            Container.Bind<PlayerTimerCleaner>().FromComponentOn(_playerInstance.Interactors).AsSingle();
            Container.Bind<PlayerTimerBuilder>().FromComponentOn(_playerInstance.Interactors).AsSingle();
        }

        private void BindDisplayData()
        {
            Container.BindInterfacesAndSelfTo<PlayerLevel>().FromComponentOn(_playerInstance.Data).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerCoins>().FromComponentOn(_playerInstance.Data).AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerGems>().FromComponentOn(_playerInstance.Data).AsSingle();
        }

        private void BindRepository()
        {
            Container.Bind<PlayerPointRepository>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerPointSaver>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerPointSetter>().AsSingle();

            Container.Bind<ISpecializationRepository>().To<SpecializationRepository>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpecializationInstaller>().AsSingle();

            Container.Bind<ICustomizationsRepository>().To<CustomizationsRepository>().AsSingle();
            Container.BindInterfacesAndSelfTo<CustomizationDataInstaller>().AsSingle();
        }
    }
}