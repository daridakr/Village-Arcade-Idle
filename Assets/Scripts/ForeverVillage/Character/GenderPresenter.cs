using System;
using UnityEngine;

namespace ForeverVillage.Scripts.Character
{
    public sealed class GenderPresenter : MonoBehaviour
    {
        [SerializeField] private CreationStepDisplay _displayer;
        [SerializeField] private ButtonDisplay _maleButton;
        [SerializeField] private ButtonDisplay _femaleButton;

        public event Action<Gender> Changed;

        private void Initialize() => OnMaleButtonClicked();

        private void OnMaleButtonClicked()
        {
            SelectMale();
            Changed?.Invoke(Gender.Male);
        }

        private void OnFemaleButtonClicked()
        {
            SelectFemale();
            Changed?.Invoke(Gender.Female);
        }

        private void SelectMale()
        {
            _maleButton.SetInteractable(false);
            _femaleButton.SetInteractable(true);
        }

        private void SelectFemale()
        {
            _femaleButton.SetInteractable(false);
            _maleButton.SetInteractable(true);
        }

        private void RegisterEvents()
        {
            _displayer.Displayed += Initialize;
            _maleButton.Clicked += OnMaleButtonClicked;
            _femaleButton.Clicked += OnFemaleButtonClicked;
        }

        private void UnregisterEvents()
        {
            _displayer.Displayed -= Initialize;
            _maleButton.Clicked -= OnMaleButtonClicked;
            _femaleButton.Clicked -= OnFemaleButtonClicked;
        }

        private void OnEnable() => RegisterEvents();
        private void OnDisable() => UnregisterEvents();
    }
}