using System;
using UnityEngine;

namespace ForeverVillage.Scripts.Character
{
    public sealed class CreationStepsDisplayer : MonoBehaviour
    {
        [SerializeField] private CreationStepDisplay[] _steps;

        private int _currentStepIndex = 0;

        public event Action Ended;

        private void OnEnable()
        {
            foreach (var step in _steps)
            {
                step.NextButtonClicked += NextStep;
                step.PreviousButtonClicked += PreviouseStep;
            }
        }

        private void Start()
        {
            if (_steps.Length > 0)
            {
                DisplayCurrentStep();
            }
        }

        private void DisplayCurrentStep()
        {
            if (CheckForCorrectIndex())
                _steps[_currentStepIndex].Display();
        }

        private void HideCurrentStep()
        {
            if (CheckForCorrectIndex())
                _steps[_currentStepIndex].Hide();
        }

        private void NextStep()
        {
            HideCurrentStep();
            _currentStepIndex++;
            DisplayCurrentStep();
        }

        private void PreviouseStep()
        {
            HideCurrentStep();
            _currentStepIndex--;
            DisplayCurrentStep();
        }

        public void ResetToLast()
        {
            _currentStepIndex = _steps.Length - 1;
            DisplayCurrentStep();
        }

        public void ResetToStart()
        {
            _currentStepIndex = 0;
            DisplayCurrentStep();
        }

        private bool CheckForCorrectIndex()
        {
            if (_currentStepIndex >= _steps.Length)
            {
                Ended?.Invoke();
                return false;
            }

            return _currentStepIndex >= 0;
        }

        private void OnDisable()
        {
            foreach (var step in _steps)
            {
                step.NextButtonClicked -= NextStep;
                step.PreviousButtonClicked -= PreviouseStep;
            }
        }
    }
}