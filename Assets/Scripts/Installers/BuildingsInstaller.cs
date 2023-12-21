using ForeverVillage.Scripts;
using UnityEngine;
using Zenject;

public class BuildingsInstaller : MonoInstaller
{
    [SerializeField] private BuildingsStoreDisplay _buildingsStore;
    [SerializeField] private UpgradeBuildingPanel _upgradePanel;
    [SerializeField] private VillagersStoreDisplay _villagersStore;

    public override void InstallBindings()
    {
        BindFactory();
        BindBuildings();
        BindResidential();
    }

    private void BindFactory()
    {
        Container.Bind<IBuildingFactory>().To<BuildingFactory>().AsSingle();
    }

    private void BindBuildings()
    {
        Container.Bind<BuildingsStoreDisplay>().FromInstance(_buildingsStore).AsSingle();
        Container.Bind<UpgradeBuildingPanel>().FromInstance(_upgradePanel).AsSingle();
    }

    private void BindResidential()
    {
        Container.BindInterfacesTo<ResidentialBuilding>().AsSingle();
        Container.Bind<VillagersStoreDisplay>().FromInstance(_villagersStore).AsSingle();
    }
}