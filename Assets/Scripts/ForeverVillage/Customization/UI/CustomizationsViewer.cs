using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ForeverVillage.Scripts
{
    public sealed class CustomizationsViewer : MonoBehaviour
    {
        [SerializeField] private CustomizationButtonView _buttonPrefab;
        [SerializeField] private Transform _buttonsContent;
        [SerializeField] private CustomizationInfoDisplayer _infoDisplayer;

        private ICustomizationsController _controller;

        private List<CustomizationPresenter> _presenters;
        private List<CustomizationButtonView> _buttonsViews;
        private CustomizationButtonView _selected;

        [Inject]
        public void Construct(ICustomizationsController controller) => _controller = controller;

        private void OnEnable()
        {
            _presenters = new List<CustomizationPresenter>();
            _buttonsViews = new List<CustomizationButtonView>();

            _controller.Initialized += CreateCustomizationButtons;
        }

        private void CreateCustomizationButtons()
        {
            ClearContent();

            ICustomization[] customizations = _controller.GetAllCustomizations();

            foreach (var model in customizations)
            {
                CustomizationButtonView view = Instantiate(_buttonPrefab, _buttonsContent);
                view.name = model.Title;
                _buttonsViews.Add(view);

                var presenter = new CustomizationPresenter(model, view);
                presenter.Initialize(_controller);
                presenter.Clicked += OnCustomButtonClicked;
                _presenters.Add(presenter);
            }

            _buttonsViews[0]?.Select();
        }


        private void OnCustomButtonClicked(CustomizationPresenter clicked)
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
                presenter.Clicked -= OnCustomButtonClicked;
            }

            _presenters.Clear();

            foreach (var view in _buttonsViews)
            {
                Destroy(view.gameObject);
            }

            _buttonsViews.Clear();
        }

        private void OnDisable() => _controller.Initialized -= CreateCustomizationButtons;
    }
}