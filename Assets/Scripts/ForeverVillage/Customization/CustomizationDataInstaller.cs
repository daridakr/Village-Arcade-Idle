using Zenject;

namespace Village
{
    public sealed class CustomizationDataInstaller :
        IInitializable
    {
        private readonly ICustomizationsRepository _repository;
        private readonly ICustomizationsController _controller;
        private readonly ICustomizableModel _customizableModel;

        public CustomizationDataInstaller(
            ICustomizationsRepository repository,
            ICustomizationsController controller,
            ICustomizableModel characterModel)
        {
            _repository = repository;
            _controller = controller;
            _customizableModel = characterModel;
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
            _controller.SetupCustomizationsFor(_customizableModel.Character);

            foreach (var customizationData in data)
            {
                ICustomization customization = _controller.GetCustomization(customizationData.Id);
                customization.Setup(customizationData.Index);
            }

            _controller.UpdateCustoms();
        }
    }
}