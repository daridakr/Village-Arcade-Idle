namespace Village
{
    public sealed class SpecializationSaver
    {
        private readonly ISpecializationRepository _repository;
        private readonly ISpecializationsController _controller;

        public SpecializationSaver(ISpecializationRepository repository, ISpecializationsController controller)
        {
            _repository = repository;
            _controller = controller;
        }

        public void Save()
        {
            Specialization specialization = (Specialization)_controller.GetSelectedSpecialization();

            var saveData = new SpecializationData
            {
                Title = specialization.Title,
                Icon = specialization.Icon,
                PrefabPath = specialization.GetModelPath(),
                WeaponTypes = specialization.WeaponTypes,
                BaseWeapons = specialization.Weapons,
                Spells = specialization.Spells
            };

            _repository.Save(saveData);
        }
    }
}