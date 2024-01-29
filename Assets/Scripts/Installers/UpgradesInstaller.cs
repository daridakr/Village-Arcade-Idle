using UnityEngine;
using Village;
using Village.Upgrades.Player;
using Zenject;

public class UpgradesInstaller : MonoInstaller
{
    [SerializeField] private UpgradesController _upgradeController;
    [SerializeField] private MovementSpeedUpgradeConfig _movementConfig;

    public override void InstallBindings()
    {
        BindUpgradesSystem();

        BindMovementUpgrade();
    }

    private void BindUpgradesSystem()
    {
        Container.BindInterfacesTo<UpgradesController>().FromInstance(_upgradeController);
        Container.BindInterfacesTo<UpgradesRepository>().AsSingle();
        Container.BindInterfacesAndSelfTo<UpgradesInteractor>().AsSingle();
    }

    private void BindMovementUpgrade()
    {
        Container.Bind<MovementSpeedUpgradeConfig>().FromInstance(_movementConfig).AsSingle();
        Container.BindInterfacesAndSelfTo<MovementSpeedUpgrade>().AsSingle();
    }
}
