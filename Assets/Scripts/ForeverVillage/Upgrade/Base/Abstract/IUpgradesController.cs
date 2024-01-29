namespace Village
{
    public interface IUpgradesController
    {
        public bool TryUpgrade(IUpgrade upgrade);
        public IUpgrade GetUpgrade(string guid);
        public IUpgrade[] GetAllUpgrades(); 
    }
}