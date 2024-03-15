using ForeverVillage;
using UnityEngine;

namespace Village
{
    public sealed class PlayerInstallerVillage : PlayerInstallerBase
    {
        [SerializeField] private PlayerReferenceVillage _playerReference;

        private PlayerReferenceVillage _playerInstance;

        #region Overrides
        protected override PlayerReferenceBase Reference => _playerReference;
        protected override PlayerReferenceBase Instance => _playerInstance;
        protected override Vector3 SpawnPosition => _playerReference.LastPosition;
        #endregion

        protected override void SpawnPlayer()
        {
            _playerInstance = Container.InstantiatePrefabForComponent<PlayerReferenceVillage>
                (Reference.gameObject, SpawnPosition, Quaternion.identity, null);
        }

        protected override void BindComponents()
        {
            base.BindComponents();

            Container.BindInterfacesAndSelfTo<TimerInteractionsController>().FromComponentOn(Instance.gameObject).AsSingle().NonLazy();

            Container.Bind<PlayerReferenceVillage>().FromInstance(_playerInstance).AsSingle().NonLazy();
            Container.Bind<PlayerTimerCleaner>().FromComponentOn(_playerInstance.Interactors).AsSingle();
            Container.Bind<PlayerTimerBuilder>().FromComponentOn(_playerInstance.Interactors).AsSingle();
        }

        protected override void BindDisplayData()
        {
            base.BindDisplayData();

            Container.BindInterfacesAndSelfTo<PlayerLevelVillage>().FromComponentOn(_playerInstance.Data).AsSingle().NonLazy();
        }

        protected override void BindRepository()
        {
            base.BindRepository();

            Container.Bind<PlayerPointRepository>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerPointSaver>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerPointSetter>().AsSingle();

            Container.BindInterfacesTo<PlayerWeaponStateControl>().AsSingle();
        }
    }
}