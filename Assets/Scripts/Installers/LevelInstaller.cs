using ForeverVillage.Scripts;
using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    //[Header("Buildings")]
    //[SerializeField] private BuildingZone _buildingZoneTempalte;
    //[SerializeField] private Transform[] _buildingZonePoints;

    //[Header("Regions")]
    //[SerializeField] private RegionReachLevelCondition _regionUnlockCondition;

    [Header("Views")]
    [SerializeField] private BuildingsStoreDisplay _buildingListView;
    [SerializeField] private VillagersStoreDisplay _vilalgersListView;

    public ResidentialView _buildingView;

    public override void InstallBindings()
    {
        BindViews();

        BindBuildingZones();
        BindRegionZones();
        BindBuildingsView();
        BindResidential();
    }

    private void BindBuildingZones()
    {
        //foreach(Transform point in _buildingZonePoints)
        //{
        //    BuildingZone buildingZone = Container.
        //        InstantiatePrefabForComponent<BuildingZone>(
        //        _buildingZoneTempalte, point.position, Quaternion.identity, point);
        //    Container.Bind<BuildingZone>().FromInstance(buildingZone);

        //    ExperiencePointGiver experienceGiver = buildingZone.GetComponent<ExperiencePointGiver>();
        //    Container.BindInterfacesTo<ExperiencePointGiver>().
        //        FromInstance(experienceGiver);

        //    buildingZone.ConstructExperience(experienceGiver);
        //}
    }

    private void BindRegionZones()
    {
        //Container.BindInterfacesTo<RegionReachLevelCondition>().
        //  FromInstance(_regionUnlockCondition).AsSingle();
    }

    private void BindViews()
    {
        Container.Bind<BuildingsStoreDisplay>().FromInstance(_buildingListView).AsSingle();
        //Container.Bind<VillagersStoreDisplay>().FromInstance(_vilalgersListView).AsSingle();
    }

    private void BindBuildingsView()
    {
        //Container.BindInterfacesTo<ResidentialViewCreator>().AsSingle();
        //Container.BindFactory<ResidentialBuilding, ResidentialView, ResidentialView.Factory>().FromComponentInNewPrefab(_buildingView);
    }

    private void BindResidential()
    {
        Container.Bind<IResidentialViewFactory>().To<ResidentialViewFactory>().AsSingle();
        Container.BindInterfacesTo<ResidentialBuilding>().AsSingle();
        Container.Bind<VillagersStoreDisplay>().FromInstance(_vilalgersListView).AsSingle();

        //Container.BindFactory<ResidentialView, ResidentialViewFactory>().FromComponentInNewPrefab(_buildingView);
    }
}