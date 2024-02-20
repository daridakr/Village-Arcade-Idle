using Zenject;

namespace Village
{
    public sealed class SpecializationInstaller :
        IInitializable
    {
        private readonly ISpecializationRepository _repository;
        private readonly SpecializationModelInitiator _modelInitiator;

        private const string _defaultSpec = ResourcesParams.Character.Specialization.MaleKnight;

        public SpecializationInstaller(ISpecializationRepository repository, SpecializationModelInitiator modelSetupper)
        {
            _repository = repository;
            _modelInitiator = modelSetupper;
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
        }
    }
}