using ForeverVillage;
using System;
using Village.Character;
using Zenject;

namespace Village
{
    public sealed class SpecializationInstaller :
        IInitializable
    {
        private readonly ISpecializationRepository _repository;
        private readonly SpecializationModelInitiator _modelInitiator;

        private readonly IPlayerWeaponInitializable _playerWeapon;
        private readonly IPlayerWeaponEquipment _playerWeaponEquipment;

        private SpecializationData _specialization;
        private SpecializationData _defaultSpecialization;

        public SpecializationData Data => _specialization;
        public event Action Initialized;

        public SpecializationInstaller(
            ISpecializationRepository repository,
            SpecializationModelInitiator modelSetupper,
            IPlayerWeaponInitializable playerWeapon,
            IPlayerWeaponEquipment playerWeaponEquipment,
            KnightSpecializationConfig defaultSpecialization)
        {
            _repository = repository;
            _modelInitiator = modelSetupper;
            _playerWeapon = playerWeapon;
            _playerWeaponEquipment = playerWeaponEquipment;

            _defaultSpecialization = new SpecializationData
            {
                BaseWeapons = defaultSpecialization.Weapons,
                Icon = defaultSpecialization.Meta.Icon,
                PrefabPath = defaultSpecialization.MalePrefabPath,
                Title = defaultSpecialization.Meta.Title,
                WeaponTypes = defaultSpecialization.WeaponTypes,
                Spells = defaultSpecialization.Spells
            };
        }

        public void Initialize()
        {
            if (_repository.Load(out SpecializationData specialization))
                _specialization = specialization;
            else
                _specialization = _defaultSpecialization;

            Install(_specialization);
        }

        private void Install(SpecializationData data)
        {
            _modelInitiator.Init(data.PrefabPath);

            _playerWeapon.Init(data.WeaponTypes);
            AssingWeaponsFor(data);

            Initialized?.Invoke();
        }

        private void AssingWeaponsFor(SpecializationData specialization)
        {
            foreach (WeaponConfig config in specialization.BaseWeapons)
            {
                Weapon weapon = config.InstantiateItem() as Weapon;
                _playerWeaponEquipment.EquipWeapon(weapon);
            }
        }
    }
}