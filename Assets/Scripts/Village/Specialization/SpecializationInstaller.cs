using ForeverVillage;
using System;
using Zenject;

namespace Village
{
    public sealed class SpecializationInstaller :
        IInitializable
    {
        private readonly ISpecializationRepository _repository;
        private readonly ISpecializationGetter _getter;

        private readonly SpecializationModelInitiator _modelInitiator;

        private readonly IPlayerWeaponInitializable _playerWeapon;
        private readonly IPlayerWeaponEquipment _playerWeaponEquipment;

        private ICharacterSpecialization _specialization;

        private const string _defaultId = "KnightSpecialization";

        public ICharacterSpecialization Specialization => _specialization;
        public event Action Initialized;

        public SpecializationInstaller(
            ISpecializationGetter getter,
            ISpecializationRepository repository,
            SpecializationModelInitiator modelSetupper,
            IPlayerWeaponInitializable playerWeapon,
            IPlayerWeaponEquipment playerWeaponEquipment)
        {
            _getter = getter;
            _repository = repository;

            _modelInitiator = modelSetupper;
            _playerWeapon = playerWeapon;
            _playerWeaponEquipment = playerWeaponEquipment;
        }

        public void Initialize()
        {
            if (_repository.Load(out SpecializationData savedData))
                SetSavedDataSpec(savedData);
            else
                SetDefaultDataSpec();

            Install();
        }

        private void Install()
        {
            _playerWeapon.Init(_specialization.Data.WeaponTypes);
            AssingWeapons();

            Initialized?.Invoke();
        }

        private void AssingWeapons()
        {
            foreach (WeaponConfig config in _specialization.Data.Weapons)
            {
                Weapon weapon = config.InstantiateItem() as Weapon;
                _playerWeaponEquipment.EquipWeapon(weapon);
            }
        }

        private void SetSavedDataSpec(SpecializationData saved)
        {
            _specialization = _getter.GetSpecialization(saved.Id);
            _modelInitiator.Init(saved.PrefabPath);
        }

        private void SetDefaultDataSpec()
        {
            _specialization = _getter.GetSpecialization(_defaultId);
            _modelInitiator.Init(_specialization.GetModelPath());
        }
    }
}