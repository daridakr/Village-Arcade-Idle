namespace Village
{
    public sealed class UpgradesInstaller
    {
        private readonly IUpgradesController _controller;

        public UpgradesInstaller(IUpgradesController controller)
        {
            _controller = controller;
        }

        public void Install(UpgradeData[] data)
        {
            foreach (var upgradeData in data)
            {
                IUpgrade upgrade = _controller.GetUpgrade(upgradeData.Id);
                upgrade.SetupLevel(upgradeData.Level);
            }
        }
    }
}