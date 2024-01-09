namespace ForeverVillage.Scripts
{
    public interface IUpgradesRepository
    {
        public bool Load(out UpgradeData[] data);
        public void Save(UpgradeData[] data);
    }
}