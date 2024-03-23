using UnityEngine;
using Village.Player;
using Village;
using Zenject;
using Village.Character;

namespace ForeverVillage
{
    public abstract class PlayerInstallerBase : MonoInstaller
    {
        [SerializeField] private MovementConfig _movementConfig;
        [SerializeField] private SpecializationsController _specializations;
        [SerializeField] private CollectableMagnitConfig _itemsMagnitConfig;

        protected abstract PlayerReferenceBase Reference { get; }
        protected abstract PlayerReferenceBase Instance { get; }
        protected abstract Vector3 SpawnPosition { get; }

        public override void InstallBindings()
        {
            BindConfigs();
            SpawnAndBindPlayer();
            BindComponents();
            BindDisplayData();
            BindRepository();
        }

        protected virtual void BindConfigs()
        {
            Container.Bind<ICustomizableCharacterFactory>().To<CustomizableCharacterFactory>().AsSingle();
            Container.Bind<SpecializationInstantiator>().AsSingle();
            Container.Bind<ISpecializationGetter>().To<SpecializationsController>().FromInstance(_specializations).AsSingle();

            Container.Bind<MovementConfig>().FromInstance(_movementConfig).AsSingle().NonLazy();
            Container.Bind<CollectableMagnitConfig>().FromInstance(_itemsMagnitConfig).AsSingle().NonLazy();
        }

        private void SpawnAndBindPlayer()
        {
            SpawnPlayer();

            Container.Bind<PlayerReferenceBase>().FromInstance(Instance).AsSingle().NonLazy();
        }

        protected virtual void BindComponents()
        {
            Container.Bind<SpecializationModelInitiator>().AsSingle();
            Container.BindInterfacesTo<AnimatedModelSetuper>().AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerCharacterModel>().FromComponentOn(Instance.Model).AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerAnimation>().FromComponentOn(Instance.Model).AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerEquipment>().FromComponentOn(Instance.Data).AsSingle().NonLazy();

            Container.Bind<PlayerMovement>().FromComponentOn(Instance.gameObject).AsSingle();
            Container.Bind<PlayerWallet>().FromComponentOn(Instance.gameObject).AsSingle();
            Container.Bind<PlayerName>().FromComponentOn(Instance.Data).AsSingle();
            Container.Bind<PlayerBuildingsList>().FromComponentOn(Instance.Data).AsSingle();
            Container.Bind<PlayerVillagersList>().FromComponentOn(Instance.Data).AsSingle();
            Container.Bind<PlayerRegionsList>().FromComponentOn(Instance.Data).AsSingle();
            Container.Bind<PlayerInventory>().FromComponentOn(Instance.Data).AsSingle();
        }

        protected virtual void BindDisplayData()
        {
            Container.BindInterfacesAndSelfTo<PlayerCoins>().FromComponentOn(Instance.Data).AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerGems>().FromComponentOn(Instance.Data).AsSingle();
        }

        protected virtual void BindRepository()
        {
            Container.Bind<ISpecializationRepository>().To<SpecializationRepository>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpecializationInstaller>().AsSingle();

            Container.Bind<ICustomizationsRepository>().To<CustomizationsRepository>().AsSingle();
            Container.BindInterfacesAndSelfTo<CustomizationDataInstaller>().AsSingle();
        }

        protected abstract void SpawnPlayer();
    }
}