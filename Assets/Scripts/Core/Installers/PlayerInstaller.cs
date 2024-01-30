using UnityEngine;
using Village.Player;
using Village;
using Zenject;

public abstract class PlayerInstaller : MonoInstaller
{
    [SerializeField] private MovementConfig _movementConfig;
    [SerializeField] private ItemsMagnitConfig _itemsMagnitConfig;

    protected abstract PlayerReference Reference { get; }
    protected abstract PlayerReference Instance { get; }
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
        Container.Bind<MovementConfig>().FromInstance(_movementConfig).AsSingle().NonLazy();
        Container.Bind<ItemsMagnitConfig>().FromInstance(_itemsMagnitConfig).AsSingle().NonLazy();
    }

    private void SpawnAndBindPlayer()
    {
        SpawnPlayer();

        Container.Bind<PlayerReference>().FromInstance(Instance).AsSingle().NonLazy();
    }

    protected virtual void BindComponents()
    {
        Container.Bind<PlayerCharacterModel>().FromComponentOn(Instance.Model).AsSingle();
        Container.Bind<PlayerMovement>().FromComponentOn(Instance.gameObject).AsSingle();
        Container.Bind<PlayerWallet>().FromComponentOn(Instance.gameObject).AsSingle();
        Container.Bind<PlayerName>().FromComponentOn(Instance.Data).AsSingle();
        Container.Bind<PlayerBuildingsList>().FromComponentOn(Instance.Data).AsSingle();
        Container.Bind<PlayerVillagersList>().FromComponentOn(Instance.Data).AsSingle();
        Container.Bind<PlayerRegionsList>().FromComponentOn(Instance.Data).AsSingle();
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
