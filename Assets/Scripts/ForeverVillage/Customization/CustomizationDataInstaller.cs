using Zenject;

namespace ForeverVillage.Scripts
{
    public sealed class CustomizationDataInstaller :
        IInitializable
    {
        private readonly ICustomizationsRepository _repository;
        private readonly ICustomizationsController _controller;
        private readonly PlayerCharacterModel _characterModel;

        public CustomizationDataInstaller(
            ICustomizationsRepository repository,
            ICustomizationsController controller,
            PlayerCharacterModel characterModel)
        {
            _repository = repository;
            _controller = controller;
            _characterModel = characterModel;
        }

        public void Initialize()
        {
            if (_repository.Load(out CustomizationData[] customizations))
            {
                Install(customizations);
            }
        }

        private void Install(CustomizationData[] data)
        {
            _controller.SetupCustomizationsFor(_characterModel.Customizable);

            foreach (var customizationData in data)
            {
                ICustomization customization = _controller.GetCustomization(customizationData.Id);
                customization.Setup(customizationData.Index);
            }

            _controller.UpdateCustoms();
        }
    }
}