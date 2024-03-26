namespace Village
{
    public sealed class SpecializationDataSaver
    {
        private readonly ISpecializationRepository _repository;
        private readonly ISpecializationsController _controller;

        public SpecializationDataSaver(ISpecializationRepository repository, ISpecializationsController controller)
        {
            _repository = repository;
            _controller = controller;
        }

        public void Save()
        {
            ICharacterSpecialization specialization = _controller.Selected;

            var saveData = new SpecializationData
            {
                Id = specialization.Id,
                PrefabPath = specialization.GetModelPath(),
                //Spells = specialization.Data.Spells
            };

            _repository.Save(saveData);
        }
    }
}