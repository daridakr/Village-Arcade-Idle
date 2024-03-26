namespace Village
{
    public interface ICustomizationsRepository
    {
        public bool Load(out CustomizationData[] data);
        public void Save(CustomizationData[] data);
    }
}