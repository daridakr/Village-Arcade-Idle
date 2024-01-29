using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Village.Character
{
    public sealed class GenderButtonsViewer : MonoBehaviour
    {
        [SerializeField] private CreationStepDisplay _displayer;
        [SerializeField] private GenderButtonView _prefab;
        [SerializeField] private Transform _content;
        [SerializeField] private GendersCatalog _catalog;

        private List<GenderPresenter> _presenters;
        private List<GenderButtonView> _views;
        private GenderButtonView _selected;

        private ISpecializationsController _specializationsController;

        [Inject]
        private void Construct(ISpecializationsController controller) => _specializationsController = controller;

        private void OnEnable()
        {
            _presenters = new List<GenderPresenter>();
            _views = new List<GenderButtonView>();

            _displayer.Displayed += CreateGenderButtons;
            _displayer.NextButtonClicked += ClearContent;
        }

        private void CreateGenderButtons()
        {
            GenderConfig[] genders = _catalog.GetAllGenders();

            foreach (var model in genders)
            {
                GenderButtonView view = Instantiate(_prefab, _content);
                view.name = model.name;
                _views.Add(view);

                var presenter = new GenderPresenter(model, view);
                presenter.Initialize(_specializationsController);
                presenter.Clicked += OnGenderButtonClicked;
                _presenters.Add(presenter);
            }

            _views[0]?.Select();
        }

        private void OnGenderButtonClicked(GenderButtonView clicked)
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
                presenter.Clicked -= OnGenderButtonClicked;
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
            _displayer.Displayed -= CreateGenderButtons;
            _displayer.NextButtonClicked -= ClearContent;
        }
    }
}