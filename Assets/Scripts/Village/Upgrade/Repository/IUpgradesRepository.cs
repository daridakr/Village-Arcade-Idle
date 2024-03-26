namespace Village
{
    public interface IUpgradesRepository
    {
        public bool Load(out UpgradeData[] data);
        public void Save(UpgradeData[] data);
    }
}