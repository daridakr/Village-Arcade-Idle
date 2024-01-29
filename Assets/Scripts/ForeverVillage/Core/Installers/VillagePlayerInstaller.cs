using UnityEngine;

namespace Village
{
    public sealed class VillagePlayerInstaller : PlayerInstaller
    {
        [SerializeField] private PlayerReference _playerReference;

        private PlayerReference _playerInstance;

        #region Overrides
        protected override global::PlayerReference Reference => _playerReference;
        protected override global::PlayerReference Instance => _playerInstance;
        protected override Vector3 SpawnPosition => _playerReference.Position;
        #endregion

        protected override void SpawnPlayer()
        {
            _playerInstance = Container.InstantiatePrefabForComponent<PlayerReference>
                (Reference.gameObject, SpawnPosition, Quaternion.identity, null);
        }

        protected override void BindComponents()
        {
            base.BindComponents();

            Container.Bind<PlayerTimerCleaner>().FromComponentOn(_playerInstance.Interactors).AsSingle();
            Container.Bind<PlayerTimerBuilder>().FromComponentOn(_playerInstance.Interactors).AsSingle();
        }

        protected override void BindDisplayData()
        {
            base.BindDisplayData();

            Container.BindInterfacesAndSelfTo<SavedPlayerLevel>().FromComponentOn(_playerInstance.Data).AsSingle().NonLazy();
        }

        protected override void BindRepository()
        {
            base.BindRepository();

            Container.Bind<PlayerPointRepository>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerPointSaver>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerPointSetter>().AsSingle();
        }
    }
}