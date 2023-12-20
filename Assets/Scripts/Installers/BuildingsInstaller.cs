using ForeverVillage.Scripts;
using UnityEngine;
using Zenject;

public class BuildingsInstaller : MonoInstaller
{
    [SerializeField] private VillagersStoreDisplay _vilalgersListView;

    public override void InstallBindings()
    {
        BindFactory();
        BindResidential();
    }

    private void BindFactory()
    {
        Container.Bind<IBuildingFactory>().To<BuildingFactory>().AsSingle();
    }

    private void BindResidential()
    {
        Container.BindInterfacesTo<ResidentialBuilding>().AsSingle();
        Container.Bind<VillagersStoreDisplay>().FromInstance(_vilalgersListView).AsSingle();
    }
}