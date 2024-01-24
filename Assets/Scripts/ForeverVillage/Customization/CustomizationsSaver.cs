namespace ForeverVillage.Scripts
{
    public sealed class CustomizationsSaver
    {
        private readonly ICustomizationsRepository _repository;
        private readonly ICustomizationsController _controller;

        public CustomizationsSaver(ICustomizationsRepository repository, ICustomizationsController controller)
        {
            _repository = repository;
            _controller = controller;
        }

        public void Save()
        {
            ICustomization[] customizations = _controller.GetAllCustomizations();

            var saveData = new CustomizationData[customizations.Length];
            int dataIterator = 0;

            foreach (var customization in customizations)
            {
                var customizationData = new CustomizationData
                {
                    Id = customization.Id,
                    Index = customization.Index
                };

                saveData[dataIterator++] = customizationData;
            }

            _repository.Save(saveData);
        }
    }
}