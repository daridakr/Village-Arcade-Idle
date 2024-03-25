using ForeverVillage;
using UnityEngine;

namespace Arena
{
    public sealed class ArenaPlayerInstaller : PlayerInstallerBase
    {
        [SerializeField] private PlayerReference _playerReference;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private PlayerHealthConfig _healthConfig;
        [SerializeField] private SpellsController _spellsController;

        private PlayerReference _playerInstance;

        #region Overrides
        protected override PlayerReferenceBase Reference => _playerReference;
        protected override PlayerReferenceBase Instance => _playerInstance;
        protected override Vector3 SpawnPosition => _spawnPoint.position;
        #endregion

        protected override void SpawnPlayer()
        {
            Container.BindInterfacesAndSelfTo<SpellsController>().FromInstance(_spellsController).AsSingle();

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

            Container.Bind<PlayerHealth>().FromComponentOn(_playerInstance.gameObject).AsSingle();
            Container.Bind<PlayerTarget>().FromComponentOn(_playerInstance.gameObject).AsSingle();
            Container.Bind<PlayerCharacterModelArena>().FromComponentOn(_playerInstance.Model).AsSingle();
            Container.Bind<PlayerMovementArena>().FromComponentOn(_playerInstance.gameObject).AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerWeaponArena>().FromComponentOn(_playerInstance.Data).AsSingle().NonLazy();
        }

        protected override void BindDisplayData()
        {
            base.BindDisplayData();

            Container.BindInterfacesAndSelfTo<PlayerLevelArena>().FromComponentOn(_playerInstance.Data).AsSingle().NonLazy();
        }

        protected override void BindRepository()
        {
            base.BindRepository();

            Container.BindInterfacesAndSelfTo<SpecializationSpellsInstaller>().AsSingle();
        }
    }
}