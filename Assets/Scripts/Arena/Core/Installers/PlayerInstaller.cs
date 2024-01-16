using ForeverVillage.Scripts;
using UnityEngine;
using Zenject;

namespace Vampire
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Player _arenaPlayerPrefab;
        [SerializeField] private Transform _playerSpawnPoint;

        private Player _playerInstance;

        public override void InstallBindings()
        {
            SpawnAndBindPlayer();

            BindComponents();
            BindDisplayData();
        }

        private void SpawnAndBindPlayer()
        {
            _playerInstance = Container.InstantiatePrefabForComponent<Player>
                (_arenaPlayerPrefab.gameObject, _playerSpawnPoint.position, Quaternion.identity, null);

            Container.Bind<Player>().FromInstance(_playerInstance).AsSingle().NonLazy();
        }

        private void BindComponents()
        {
            Container.Bind<PlayerMovement>().FromInstance(_playerInstance.Movement).AsSingle();
            //Container.Bind<PlayerWallet>().FromInstance(_playerInstance.Wallet).AsSingle();
        }

        private void BindDisplayData()
        {
            Container.BindInterfacesAndSelfTo<PlayerCoins>().FromInstance(_playerInstance.Coins).AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerGems>().FromInstance(_playerInstance.Gems).AsSingle();
        }
    }
}