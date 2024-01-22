using ForeverVillage.Scripts.Character;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ForeverVillage.Scripts
{
    public sealed class CustomizationButtonsViewer : MonoBehaviour
    {
        [SerializeField] private CreationStepDisplay _previousStep;
        [SerializeField] private CustomizationButtonView _customButtonViewPrefab;
        [SerializeField] private Transform _content;

        private List<CustomizationPresenter> _presenters;
        private List<CustomizationButtonView> _buttonViews;

        private ICustomizationsController _customizationsController;

        [Inject]
        public void Construct(ICustomizationsController controller)
        {
            _customizationsController = controller;
        }

        private void OnEnable()
        {
            _presenters = new List<CustomizationPresenter>();
            _buttonViews = new List<CustomizationButtonView>();

            _previousStep.NextButtonClicked += CreateCustomizationButtons;
        }

        private void CreateCustomizationButtons()
        {
            ClearContent();

            var charcter = FindAnyObjectByType<CustomizableCharacter>(); // test!!!
            _customizationsController.SetupCustomizationsFor(charcter);
            ICustomization[] customizations = _customizationsController.GetAllCustomizations();

            foreach (var model in customizations)
            {
                CustomizationButtonView view = Instantiate(_customButtonViewPrefab, _content);
                _buttonViews.Add(view);

                var presenter = new CustomizationPresenter(model, view);
                presenter.Initialize(_customizationsController);
                _presenters.Add(presenter);
            }

            _buttonViews[0].Select();
        }

        private void ClearContent()
        {
            foreach (var presenter in _presenters)
            {
                presenter.Dispose();
            }

            _presenters.Clear();

            foreach (var view in _buttonViews)
            {
                Destroy(view.gameObject);
            }

            _buttonViews.Clear();
        }

        private void OnDisable()
        {
            _previousStep.NextButtonClicked -= CreateCustomizationButtons;
        }
    }
}