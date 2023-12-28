namespace ForeverVillage.Scripts
{
    public interface IPlayerUpgrades
    {
        public bool CanUpgrade(IPlayerUpgrade upgrade);
        public void Upgrade(IPlayerUpgrade upgrade);
        public IPlayerUpgrade GetUpgrade(string guid);
        public IPlayerUpgrade[] GetAllUpgrades(); 
    }
}