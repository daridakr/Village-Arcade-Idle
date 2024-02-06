using UnityEngine;

namespace Arena
{
    public sealed class ArenaPlayerInstaller : PlayerInstaller
    {
        [SerializeField] private PlayerHealthConfig _healthConfig;
        [SerializeField] private PlayerReference _playerReference;
        [SerializeField] private Transform _spawnPoint;

        private PlayerReference _playerInstance;

        #region Overrides
        protected override global::PlayerReference Reference => _playerReference;
        protected override global::PlayerReference Instance => _playerInstance;
        protected override Vector3 SpawnPosition => _spawnPoint.position;
        #endregion

        protected override void SpawnPlayer()
        {
            _playerInstance = Container.InstantiatePrefabForComponent<PlayerReference>
                (Reference.gameObject, SpawnPosition, Quaternion.identity, null);
        }

        protected override void BindConfigs()
        {
            base.BindConfigs();

            Container.Bind<PlayerHealthConfig>().FromInstance(_healthConfig).AsSingle().NonLazy();
        }

        protected override void BindComponents()
        {
            base.BindComponents();

            Container.Bind<PlayerHealth>().FromComponentOn(Instance.gameObject).AsSingle();
            Container.Bind<PlayerCharacterModelArena>().FromComponentOn(Instance.Model).AsSingle();
            Container.Bind<PlayerMovementArena>().FromComponentOn(Instance.gameObject).AsSingle();
        }

        protected override void BindDisplayData()
        {
            base.BindDisplayData();

            Container.BindInterfacesAndSelfTo<PlayerLevel>().FromComponentOn(_playerInstance.Data).AsSingle().NonLazy();
        }
    }
}