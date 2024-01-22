using ForeverVillage.Scripts.Character;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ForeverVillage.Scripts
{
    public sealed class SpecializationButtonsViewer : MonoBehaviour
    {
        [SerializeField] private CreationStepDisplay _displayer;
        [SerializeField] private SpecializationButtonView _prefab;
        [SerializeField] private Transform _content;
        [SerializeField] private ISpecializationsController _controller;

        private List<SpecializationPresenter> _presenters;
        private List<SpecializationButtonView> _views;
        private SpecializationButtonView _selected;

        [Inject]
        public void Construct(ISpecializationsController controller) => _controller = controller;

        private void OnEnable()
        {
            _presenters = new List<SpecializationPresenter>();
            _views = new List<SpecializationButtonView>();

            _controller.Initialized += CreateSpecializationButtons;
            //_displayer.NextButtonClicked += ClearContent;
        }

        private void CreateSpecializationButtons()
        {
            ClearContent();

            ISpecialization[] specializations = _controller.GetAllSpecializations();

            foreach (var model in specializations)
            {
                SpecializationButtonView view = Instantiate(_prefab, _content);
                view.name = model.Title;
                _views.Add(view);

                var presenter = new SpecializationPresenter(model, view);
                presenter.Initialize(_controller);
                presenter.Clicked += OnSpecButtonClicked;
                _presenters.Add(presenter);
            }

            _views[0].Select();
        }

        private void OnSpecButtonClicked(SpecializationButtonView clicked)
        {
            if (_selected != null)
                _selected.SetUnclicked();

            clicked.SetClicked();
            _selected = clicked;
        }

        private void ClearContent()
        {
            foreach (var presenter in _presenters)
            {
                presenter.Dispose();
                presenter.Clicked -= OnSpecButtonClicked;
            }

            _presenters.Clear();

            foreach (var view in _views)
            {
                Destroy(view.gameObject);
            }

            _views.Clear();
        }

        private void OnDisable()
        {
            _displayer.Displayed -= CreateSpecializationButtons;
            _displayer.NextButtonClicked -= ClearContent;
        }
    }
}