using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    [Header("Buildings")]
    [SerializeField] private BuildingZone _buildingZoneTempalte;
    [SerializeField] private Transform[] _buildingZonePoints;

    [Header("Views")]
    [SerializeField] private BuildingListView _buildingListView;

    public override void InstallBindings()
    {
        BindViews();
        BindBuildingZones();
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

    private void BindViews()
    {
        Container.Bind<BuildingListView>().FromInstance(_buildingListView).AsSingle();
    }
}