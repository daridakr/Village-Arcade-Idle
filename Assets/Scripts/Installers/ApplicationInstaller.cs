using ForeverVillage.Scripts;
using UnityEngine;
using Zenject;

public class ApplicationInstaller : MonoInstaller
{
    [SerializeField] private ApplicationStatus _appStatus;

    [Header("Game Save Data Listeners")]
    [SerializeField] private PlayerCoins _playerCoins;
    [SerializeField] private PlayerGems _playerGems;

    public override void InstallBindings()
    {
        BindGameSaver();
    }

    private void BindGameSaver()
    {
        Container.Bind<ApplicationStatus>().FromInstance(_appStatus).AsSingle();
        Container.BindInterfacesAndSelfTo<GameSaver>().AsSingle();

        BindGameSaveDataListeners();
    }

    private void BindGameSaveDataListeners()
    {
        Container.BindInterfacesTo<PlayerCoins>().FromInstance(_playerCoins).AsSingle();
        Container.BindInterfacesTo<PlayerGems>().FromInstance(_playerGems).AsSingle();
    }
}