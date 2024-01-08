namespace ForeverVillage.Scripts
{
    public sealed class UpgradesLoader
    {
        private readonly IUpgradesRepository _repository;

        public UpgradesLoader(IUpgradesRepository repository)
        {
            _repository = repository;
        }

        public UpgradeData[] Load()
        {
            UpgradeData[] data;
            _repository.Load(out data);
            return data;
        }
    }
}