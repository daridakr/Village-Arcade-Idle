using Zenject;

namespace ForeverVillage.Scripts
{
    public sealed class SpecializationInstaller :
        IInitializable
    {
        private ISpecializationRepository _repository;
        private readonly CharacterModel _characterModel;

        public SpecializationInstaller(ISpecializationRepository repository, CharacterModel characterModel)
        {
            _repository = repository;
            _characterModel = characterModel;
        }

        public void Initialize()
        {
            if (_repository.Load(out SpecializationData specialization))
            {
                Install(specialization);
            }
        }

        public void Install(SpecializationData data)
        {
            _characterModel.SetupPrefabPath(data.PrefabPath);
        }
    }
}