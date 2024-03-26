using System;
using Village.Character;

namespace Village
{
    public sealed class SpecializationPresenter
    {
        private readonly ICharacterSpecialization _model;
        private readonly SpecializationButtonView _view;

        private ISpecializationsController _controller;
        private ICustomizationsController _customizationsController;

        public SpecializationMetadata Meta => _model.Data.Meta;
        public SpecializationButtonView Button => _view;

        public event Action<SpecializationPresenter> Clicked;

        public SpecializationPresenter(ICharacterSpecialization model, SpecializationButtonView buttonView)
        {
            _model = model;
            _view = buttonView;
        }

        public void Initialize(ISpecializationsController specializationsController, ICustomizationsController customizationsController)
        {
            _controller = specializationsController;
            _customizationsController = customizationsController;

            _view.SetIcon(Meta.Icon);
            _view.Selected += OnSpecClicked;
        }

        private void OnSpecClicked(SpecializationButtonView selected)
        {
            ICustomizableCharacter instance = _controller.SelectSpecialization(_model);
            _customizationsController.SetupCustomizationsFor(instance);

            Clicked?.Invoke(this);
        }

        public void Dispose() => _view.Selected -= OnSpecClicked;
    }
}