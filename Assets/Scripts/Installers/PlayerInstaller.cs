using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerLevel _level;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private PlayerTimerCleaner _cleaner;
    [SerializeField] private PlayerTimerBuilder _builder;

    public override void InstallBindings()
    {
        // temp
        BindPlayerBuildingInteractions();
        BindVillageInfo();
    }

    private void BindPlayerBuildingInteractions()
    {
        Container.Bind<PlayerTimerCleaner>().FromInstance(_cleaner).AsSingle();
        Container.Bind<PlayerTimerBuilder>().FromInstance(_builder).AsSingle();
    }

    private void BindVillageInfo()
    {
        Container.Bind<PlayerLevel>().FromInstance(_level).AsSingle();
    }

    private void SpawnPlayer()
    {
        //Player player = Container
        //            .InstantiatePrefabForComponent<Player>(_player, _spawnPoint.position, Quaternion.identity, null);

        //Container.Bind<Player>().FromInstance(player).AsSingle();
    }
}
