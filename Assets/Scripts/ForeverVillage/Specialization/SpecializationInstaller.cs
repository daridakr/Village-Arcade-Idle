using Zenject;

namespace ForeverVillage.Scripts
{
    public sealed class SpecializationInstaller :
        IInitializable
    {
        private readonly ISpecializationRepository _repository;
        private readonly PlayerCharacterModel _characterModel;

        private const string _defaultSpec = ResourcesParams.Character.Specialization.MaleKnight;

        public SpecializationInstaller(ISpecializationRepository repository, PlayerCharacterModel characterModel)
        {
            _repository = repository;
            _characterModel = characterModel;
        }

        public void Initialize()
        {
            if (_repository.Load(out SpecializationData specialization))
                Install(specialization);
            else
                _characterModel.Setup(_defaultSpec);
        }

        private void Install(SpecializationData data)
        {
            _characterModel.Setup(data.PrefabPath);
        }
    }
}