namespace ForeverVillage.Scripts
{
    public interface ICustomizationsRepository
    {
        public bool Load(out CustomizationData[] data);
        public void Save(CustomizationData[] data);
    }
}