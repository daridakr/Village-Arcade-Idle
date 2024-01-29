using UnityEngine;

namespace Vampire
{
    public sealed class PlayerInstaller : global::PlayerInstaller
    {
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

        protected override void BindComponents()
        {
            base.BindComponents();

            //Container.Bind<PlayerCharacterModel>().FromComponentOn(_playerInstance.Model).AsSingle();
        }

        protected override void BindDisplayData()
        {
            base.BindDisplayData();

            Container.BindInterfacesAndSelfTo<PlayerLevel>().FromComponentOn(_playerInstance.Data).AsSingle().NonLazy();
        }
    }
}