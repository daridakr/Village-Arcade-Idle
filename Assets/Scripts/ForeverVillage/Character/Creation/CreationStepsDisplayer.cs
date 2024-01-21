using UnityEngine;

namespace ForeverVillage.Scripts.Character
{
    public sealed class CreationStepsDisplayer : MonoBehaviour
    {
        [SerializeField] private CreationStepDisplay[] _steps;

        private int _currentStepIndex = 0;

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

        private bool CheckForCorrectIndex()
        {
            return _currentStepIndex >= 0 && _currentStepIndex < _steps.Length;
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