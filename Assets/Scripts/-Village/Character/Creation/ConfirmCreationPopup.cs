using System;
using UnityEngine;

namespace Village.Character
{
    public class ConfirmCreationPopup : CanvasAnimatedView
    {
        [SerializeField] private CreationStepsDisplayer _stepsDisplayer;
        [SerializeField] private ButtonDisplay _cancelButton;
        [SerializeField] private ButtonDisplay _playButton;

        public event Action PlayButtonClicked;

        private void OnEnable()
        {
            _stepsDisplayer.Ended += Display;
        }

        public override void Display()
        {
            base.Display();
            _playButton.Clicked += OnPlayButtonClicked;
            _cancelButton.Clicked += OnCancelButtonClicked;
        }

        public override void Hide()
        {
            base.Hide();
            _playButton.Clicked -= OnPlayButtonClicked;
            _cancelButton.Clicked += OnCancelButtonClicked;
        }

        private void OnCancelButtonClicked()
        {
            Hide();
            _stepsDisplayer.ResetToLast();
        }

        private void OnPlayButtonClicked()
        {
            Hide();
            PlayButtonClicked?.Invoke();
        }

        private void OnDisable()
        {
            _stepsDisplayer.Ended -= Display;
        }
    }
}