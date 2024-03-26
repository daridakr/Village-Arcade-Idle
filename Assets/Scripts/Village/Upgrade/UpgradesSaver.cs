namespace Village
{
    public sealed class UpgradesSaver
    {
        private readonly IUpgradesRepository _repository;
        private readonly IUpgradesController _controller;

        public UpgradesSaver(IUpgradesRepository repository, IUpgradesController controller)
        {
            _repository = repository;
            _controller = controller;
        }

        public void Save()
        {
            IUpgrade[] upgrades = _controller.GetAllUpgrades();

            var saveData = new UpgradeData[upgrades.Length];
            int dataIterator = 0;

            foreach (var upgrade in upgrades)
            {
                var upgradeData = new UpgradeData
                {
                    Id = upgrade.Id,
                    Level = upgrade.Level
                };

                saveData[dataIterator++] = upgradeData;
            }

            _repository.Save(saveData);
        }
    }
}