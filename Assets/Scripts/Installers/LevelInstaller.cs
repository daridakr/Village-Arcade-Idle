using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    [Header("Player")]
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Player _player;
    [SerializeField] private BuildingCleaner _cleaner;
    [SerializeField] private BuildingBuilder _builder;

    [Header("Buildings")]
    [SerializeField] private BuildingZone _buildingZoneTempalte;
    [SerializeField] private Transform[] _buildingZonePoints;

    [Header("Views")]
    [SerializeField] private BuildingListView _buildingListView;

    [Header("Services")]
    [SerializeField] private JoystickInputControl _playerControl;

    public override void InstallBindings()
    {
        BindInputControl();
        BindPlayer();
        BindViews();
        BindBuildingZones();
    }

    private void BindInputControl()
    {
        Container.BindInterfacesTo<JoystickInputControl>().
            FromComponentInNewPrefab(_playerControl).AsSingle();
    }

    private void BindPlayer()
    {
        Container.Bind<BuildingCleaner>().FromInstance(_cleaner).AsSingle();
        Container.Bind<BuildingBuilder>().FromInstance(_builder).AsSingle();

        //Player player = Container
        //            .InstantiatePrefabForComponent<Player>(_player, _spawnPoint.position, Quaternion.identity, null);

        //Container.Bind<Player>().FromInstance(player).AsSingle();
    }

    private void BindViews()
    {
        Container.Bind<BuildingListView>().FromInstance(_buildingListView).AsSingle();
    }

    private void BindBuildingZones()
    {
        foreach(Transform point in _buildingZonePoints)
        {
            BuildingZone buildingZone = Container.
                InstantiatePrefabForComponent<BuildingZone>(
                _buildingZoneTempalte, point.position, Quaternion.identity, point);

            Container.Bind<BuildingZone>().FromInstance(buildingZone);
        }
    }
}