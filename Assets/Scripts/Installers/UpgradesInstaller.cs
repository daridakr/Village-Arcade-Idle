using ForeverVillage.Scripts;
using ForeverVillage.Scripts.Upgrades.Player;
using UnityEngine;
using Zenject;

public class UpgradesInstaller : MonoInstaller
{
    [SerializeField] private MovementSpeedUpgradeConfig _movementConfig;

    public override void InstallBindings()
    {
        BindPlayerUpgrades();
    }

    private void BindPlayerUpgrades()
    {
        Container.Bind<MovementSpeedUpgradeConfig>().FromInstance(_movementConfig).AsSingle();
        Container.BindInterfacesAndSelfTo<MovementSpeedUpgrade>().AsSingle();
    }
}
