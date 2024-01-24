namespace ForeverVillage.Scripts
{
    public sealed class CustomizationsRepository : DataArrayRepository<CustomizationData>,
        ICustomizationsRepository
    {
        protected override string _key => "CustomizationsData";

        public bool Load(out CustomizationData[] data)
        {
            return LoadData(out data);
        }

        public void Save(CustomizationData[] data)
        {
            SaveData(data);
        }
    }
}