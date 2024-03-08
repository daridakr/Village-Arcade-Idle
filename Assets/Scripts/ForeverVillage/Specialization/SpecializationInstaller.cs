using ForeverVillage;
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

        private const string _defaultSpec = ResourcesParams.Character.Specialization.MaleKnight;

        public SpecializationInstaller(
            ISpecializationRepository repository,
            SpecializationModelInitiator modelSetupper,
            IPlayerWeaponInitializable playerWeapon,
            IPlayerWeaponEquipment playerWeaponEquipment)
        {
            _repository = repository;
            _modelInitiator = modelSetupper;
            _playerWeapon = playerWeapon;
            _playerWeaponEquipment = playerWeaponEquipment;
        }

        public void Initialize()
        {
            if (_repository.Load(out SpecializationData specialization))
                Install(specialization);
            else
                _modelInitiator.Init(_defaultSpec);
        }

        private void Install(SpecializationData data)
        {
            _modelInitiator.Init(data.PrefabPath);

            _playerWeapon.Init(data.WeaponTypes);
            AssingWeapons(data);
        }

        private void AssingWeapons(SpecializationData specialization)
        {
            foreach (WeaponConfig config in specialization.BaseWeapons)
            {
                Weapon weapon = config.InstantiateItem() as Weapon;
                _playerWeaponEquipment.EquipWeapon(weapon);
            }
        }
    }
}