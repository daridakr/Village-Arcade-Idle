using UnityEngine;
using Zenject;

public class ApplicationInstaller : MonoInstaller
{
    [SerializeField] private ApplicationStatus _appStatus;

    public override void InstallBindings()
    {
        BindGameSaver();
    }

    private void BindGameSaver()
    {
        Container.Bind<ApplicationStatus>().FromInstance(_appStatus).AsSingle();
        Container.BindInterfacesAndSelfTo<GameSaver>().AsSingle();
    }
}