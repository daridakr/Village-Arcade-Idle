namespace ForeverVillage.Scripts
{
    public sealed class UpgradesRepository : DataArrayRepository<UpgradeData>, IUpgradesRepository
    {
        protected override string _key => "UpgradesData";

        public bool Load(out UpgradeData[] data)
        {
            return LoadData(out data);
        }

        public void Save(UpgradeData[] data)
        {
            SaveData(data);
        }
    }
}