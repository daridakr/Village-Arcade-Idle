using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ForeverVillage.Scripts
{
    public sealed class SpecializationsViewer : MonoBehaviour
    {
        [SerializeField] private SpecializationButtonView _buttonPrefab;
        [SerializeField] private Transform _buttonsContent;
        [SerializeField] private SpecializationInfoDisplayer _infoDisplayer;
        
        private ISpecializationsController _controller;
        private ICustomizationsController _customizationsController;

        private List<SpecializationPresenter> _presenters;
        private List<SpecializationButtonView> _buttonsViews;
        private SpecializationButtonView _selected;

        [Inject]
        private void Construct(ISpecializationsController controller, ICustomizationsController customizationsController)
        {
            _controller = controller;
            _customizationsController = customizationsController;
        }

        private void OnEnable()
        {
            _presenters = new List<SpecializationPresenter>();
            _buttonsViews = new List<SpecializationButtonView>();

            _controller.Initialized += CreateSpecializationButtons;
        }

        private void CreateSpecializationButtons()
        {
            ClearContent();

            ISpecialization[] specializations = _controller.GetAllSpecializations();

            foreach (var model in specializations)
            {
                SpecializationButtonView view = Instantiate(_buttonPrefab, _buttonsContent);
                view.name = model.Title;
                _buttonsViews.Add(view);

                var presenter = new SpecializationPresenter(model, view);
                presenter.Initialize(_controller, _customizationsController);
                presenter.Clicked += OnSpecButtonClicked;
                _presenters.Add(presenter);
            }

            _buttonsViews[0].Select();
        }

        private void OnSpecButtonClicked(SpecializationPresenter clicked)
        {
            if (_selected != null)
                _selected.SetUnclicked();

            clicked.Button.SetClicked();
            _selected = clicked.Button;

            _infoDisplayer.Display(clicked.Model);
        }

        private void ClearContent()
        {
            foreach (var presenter in _presenters)
            {
                presenter.Dispose();
                presenter.Clicked -= OnSpecButtonClicked;
            }

            _presenters.Clear();

            foreach (var view in _buttonsViews)
            {
                Destroy(view.gameObject);
            }

            _buttonsViews.Clear();
        }

        private void OnDisable() => _controller.Initialized -= CreateSpecializationButtons;
    }
}