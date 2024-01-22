using System;
using UnityEngine;

namespace ForeverVillage.Scripts.Character
{
    public sealed class CreationStepDisplay : CanvasAnimatedView
    {
        [SerializeField] private ButtonDisplay _nextButton;
        [SerializeField] private ButtonDisplay _previousButton;

        public event Action NextButtonClicked;
        public event Action PreviousButtonClicked;

        private void OnEnable()
        {
            _nextButton.Clicked += OnNextButtonClicked;
            _previousButton.Clicked += OnPreviousButtonClicked;
        }

        private void OnNextButtonClicked()
        {
            NextButtonClicked?.Invoke();
        }

        private void OnPreviousButtonClicked()
        {
            PreviousButtonClicked?.Invoke();
        }

        private void OnDisable()
        {
            _nextButton.Clicked -= OnNextButtonClicked;
            _previousButton.Clicked -= OnPreviousButtonClicked;
        }
    }
}