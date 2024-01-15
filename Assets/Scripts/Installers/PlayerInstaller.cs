using ForeverVillage.Scripts;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private Player _player;
    [SerializeField] private Transform _spawnPoint;

    public override void InstallBindings()
    {
        SpawnAndBindPlayer();
    }

    private void SpawnAndBindPlayer()
    {
        Player playerInstance =
            Container.InstantiatePrefabForComponent<Player>(_player.gameObject, _spawnPoint.position, Quaternion.identity, null);

        Container.Bind<Player>().FromInstance(playerInstance).AsSingle().NonLazy();

        Container.Bind<PlayerMovement>().FromInstance(playerInstance.Movement).AsSingle();
        Container.Bind<PlayerTimerCleaner>().FromInstance(playerInstance.Cleaner).AsSingle();
        Container.Bind<PlayerTimerBuilder>().FromInstance(playerInstance.Builder).AsSingle();
        Container.Bind<PlayerWallet>().FromInstance(playerInstance.Wallet).AsSingle();
        Container.Bind<PlayerBuildingsList>().FromInstance(playerInstance.Buildings).AsSingle();
        Container.Bind<PlayerVillagersList>().FromInstance(playerInstance.Villagers).AsSingle();
        Container.Bind<PlayerRegionsList>().FromInstance(playerInstance.Regions).AsSingle();

        Container.BindInterfacesAndSelfTo<PlayerLevel>().FromInstance(playerInstance.Level).AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PlayerCoins>().FromInstance(playerInstance.Coins).AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerGems>().FromInstance(playerInstance.Gems).AsSingle();
    }
}