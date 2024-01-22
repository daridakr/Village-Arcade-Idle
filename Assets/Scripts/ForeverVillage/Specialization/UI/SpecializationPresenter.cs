using System;

namespace ForeverVillage.Scripts
{
    public sealed class SpecializationPresenter
    {
        private readonly ISpecialization _model;
        private readonly SpecializationButtonView _view;

        private ISpecializationsController _controller;

        public ISpecialization Model => _model;
        public SpecializationButtonView Button => _view;

        public event Action<SpecializationPresenter> Clicked;

        public SpecializationPresenter(ISpecialization specialization, SpecializationButtonView buttonView)
        {
            _model = specialization;
            _view = buttonView;
        }

        public void Initialize(ISpecializationsController specializationsController)
        {
            _controller = specializationsController;

            _view.SetIcon(_model.Icon);
            _view.Selected += OnSpecClicked;
        }

        private void OnSpecClicked(SpecializationButtonView selected)
        {
            _controller.SelectSpecialization(_model);

            Clicked?.Invoke(this);
        }

        public void Dispose()
        {
            _view.Selected -= OnSpecClicked;
        }
    }
}