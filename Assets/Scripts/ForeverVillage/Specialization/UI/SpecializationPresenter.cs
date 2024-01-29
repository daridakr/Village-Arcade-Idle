using System;
using UnityEngine;

namespace Village
{
    public sealed class SpecializationPresenter
    {
        private readonly ISpecialization _model;
        private readonly SpecializationButtonView _view;

        private ISpecializationsController _controller;
        private ICustomizationsController _customizationsController;

        public ISpecialization Model => _model;
        public SpecializationButtonView Button => _view;

        public event Action<SpecializationPresenter> Clicked;

        public SpecializationPresenter(ISpecialization specialization, SpecializationButtonView buttonView)
        {
            _model = specialization;
            _view = buttonView;
        }

        public void Initialize(ISpecializationsController specializationsController, ICustomizationsController customizationsController)
        {
            _controller = specializationsController;
            _customizationsController = customizationsController;

            _view.SetIcon(_model.Icon);
            _view.Selected += OnSpecClicked;
        }

        private void OnSpecClicked(SpecializationButtonView selected)
        {
            MonoBehaviour instance = _controller.SelectSpecialization(_model);
            _customizationsController.SetupCustomizationsFor(instance);

            Clicked?.Invoke(this);
        }

        public void Dispose() => _view.Selected -= OnSpecClicked;
    }
}