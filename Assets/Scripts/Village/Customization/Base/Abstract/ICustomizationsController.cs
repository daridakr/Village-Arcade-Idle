using Village.Character;

namespace Village
{
    public interface ICustomizationsController : IInitilizable
    {
        public void SetupCustomizationsFor(ICustomizableCharacter customizable);
        public void SelectCustom(ICustomization customization);
        public void NextCurrent();
        public void PreviousCurrent();
        public void UpdateCustoms();
        public ICustomization GetCustomization(string guid);
        public ICustomization[] GetAllCustomizations();
    }
}