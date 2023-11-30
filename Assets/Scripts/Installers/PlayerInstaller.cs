using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private Player _player;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private BuildingCleaner _cleaner;
    [SerializeField] private BuildingBuilder _builder;
    [SerializeField] private JoystickInputControl _playerControl;

    public override void InstallBindings()
    {
        // temp
        BindPlayerBuildingInteractions();

        BindInputControl();
    }

    private void BindPlayerBuildingInteractions()
    {
        Container.Bind<BuildingCleaner>().FromInstance(_cleaner).AsSingle();
        Container.Bind<BuildingBuilder>().FromInstance(_builder).AsSingle();
    }

    private void BindInputControl()
    {
        Container.BindInterfacesTo<JoystickInputControl>().
          FromComponentInNewPrefab(_playerControl).AsSingle();
    }

    private void SpawnPlayer()
    {
        //Player player = Container
        //            .InstantiatePrefabForComponent<Player>(_player, _spawnPoint.position, Quaternion.identity, null);

        //Container.Bind<Player>().FromInstance(player).AsSingle();
    }
}
