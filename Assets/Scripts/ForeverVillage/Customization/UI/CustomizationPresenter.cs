namespace ForeverVillage.Scripts
{
    public sealed class CustomizationPresenter
    {
        private ICustomizationsController _customizationsController;
        private readonly ICustomization _customization;
        private readonly CustomizationButtonView _view;

        private CustomizationButtonView _selected;

        public CustomizationPresenter(ICustomization customization, CustomizationButtonView buttonView)
        {
            _customization = customization;
            _view = buttonView;
        }

        public void Initialize(ICustomizationsController customizationController)
        {
            _customizationsController = customizationController;

            _view.SetIcon(_customization.Icon);
            _view.Selected += OnCustomClicked;
        }

        private void OnCustomClicked(CustomizationButtonView selected)
        {
            if (_selected != null)
                _selected.Unselect();

            selected.Select();
            _selected = selected;

            _customizationsController.SelectCustom(_customization);
        }

        public void Dispose()
        {
            _view.Selected -= OnCustomClicked;
        }
    }
}