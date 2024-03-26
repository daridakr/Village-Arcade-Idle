using System;

namespace Village
{
    public sealed class CustomizationPresenter
    {
        private readonly ICustomization _model;
        private readonly CustomizationButtonView _view;

        private ICustomizationsController _controller;

        public ICustomization Model => _model;
        public CustomizationButtonView Button => _view;

        public event Action<CustomizationPresenter> Clicked;

        public CustomizationPresenter(ICustomization customization, CustomizationButtonView buttonView)
        {
            _model = customization;
            _view = buttonView;
        }

        public void Initialize(ICustomizationsController customizationController)
        {
            _controller = customizationController;

            _view.SetIcon(_model.Icon);
            _view.Selected += OnCustomClicked;
        }

        private void OnCustomClicked(CustomizationButtonView selected)
        {
            _controller.SelectCustom(_model);

            Clicked?.Invoke(this);
        }

        public void Dispose() => _view.Selected -= OnCustomClicked;
    }
}