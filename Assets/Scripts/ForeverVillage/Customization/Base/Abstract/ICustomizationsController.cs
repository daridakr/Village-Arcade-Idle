namespace ForeverVillage.Scripts
{
    public interface ICustomizationsController
    {
        public void SelectCustom(ICustomization customization);
        public ICustomization[] GetAllCustomizations();
    }
}