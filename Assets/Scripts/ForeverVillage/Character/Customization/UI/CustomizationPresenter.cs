using ForeverVillage.Scripts.Character;
using static ResourcesParams.Character;

namespace ForeverVillage.Scripts
{
    public sealed class CustomizationPresenter
    {
        private readonly ICustomization _customization;
        private readonly CustomizationButtonView _view;
        private CustomizationsController _customizationsController;

        private CustomizationButtonView _selected;

        public CustomizationPresenter(ICustomization customization, CustomizationButtonView buttonView)
        {
            _customization = customization;
            _view = buttonView;
        }

        public void Initialize(CustomizationsController customizationController)
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