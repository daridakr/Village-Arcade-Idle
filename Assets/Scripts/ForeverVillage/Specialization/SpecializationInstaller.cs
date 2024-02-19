using Zenject;

namespace Village
{
    public sealed class SpecializationInstaller :
        IInitializable
    {
        private readonly ISpecializationRepository _repository;
        private readonly SpecializationModelSetuper _modelSetuper;

        private const string _defaultSpec = ResourcesParams.Character.Specialization.MaleKnight;

        public SpecializationInstaller(ISpecializationRepository repository, SpecializationModelSetuper modelSetupper)
        {
            _repository = repository;
            _modelSetuper = modelSetupper;
        }

        public void Initialize()
        {
            if (_repository.Load(out SpecializationData specialization))
                Install(specialization);
            else
                _modelSetuper.Setup(_defaultSpec);
        }

        private void Install(SpecializationData data)
        {
            _modelSetuper.Setup(data.PrefabPath);
        }
    }
}