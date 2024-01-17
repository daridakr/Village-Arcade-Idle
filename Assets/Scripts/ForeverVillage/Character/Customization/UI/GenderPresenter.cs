using System;
using UnityEngine;
using Zenject;

namespace ForeverVillage.Scripts.Character
{
    public sealed class GenderPresenter : MonoBehaviour
    {
        [SerializeField] private CreationStepDisplay _displayer;
        [SerializeField] private ButtonDisplay _maleButton;
        [SerializeField] private ButtonDisplay _femaleButton;

        private Character _characterPrefab;
        private Character _characterInstance;

        private CharacterCreator _creator;
        private CharacterLoader _loader;

        public event Action<Character> Initialized;
        public event Action<Character> Changed;

        [Inject]
        public void Construct(CharacterCreator creator)
        {
            _creator = creator;
        }

        private void OnEnable()
        {
            _displayer.Displayed += Initialize;
            _maleButton.Clicked += OnMaleButtonClicked;
            _femaleButton.Clicked += OnFemaleButtonClicked;
        }

        private void Awake()
        {
            _loader = new CharacterLoader();
        }

        private void Initialize()
        {
            SelectMale();
            Initialized?.Invoke(_characterInstance);
        }

        private void SelectMale()
        {
            _characterPrefab = _loader.LoadMaleCharacter();
            Present(Gender.Male);

            _maleButton.SetInteractable(false);
            _femaleButton.SetInteractable(true);
        }

        private void OnMaleButtonClicked()
        {
            SelectMale();
            Changed?.Invoke(_characterInstance);
        }

        private void SelectFemale()
        {
            _characterPrefab = _loader.LoadFemaleCharacter();
            Present(Gender.Female);

            _femaleButton.SetInteractable(false);
            _maleButton.SetInteractable(true);
        }

        private void OnFemaleButtonClicked()
        {
            SelectFemale();
            Changed?.Invoke(_characterInstance);
        }

        private void Present(Gender gender)
        {
            if (_characterInstance != null)
                Destroy(_characterInstance.gameObject);

            _characterInstance = _creator.Create(_characterPrefab);
            _characterInstance.SetGender(gender);
        }

        private void OnDisable()
        {
            _displayer.Displayed -= Initialize;
            _maleButton.Clicked -= OnMaleButtonClicked;
            _femaleButton.Clicked -= OnFemaleButtonClicked;
        }
    }
}