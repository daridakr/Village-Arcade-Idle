using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ForeverVillage.Scripts.Character
{
    public sealed class SpecializationPresenter : MonoBehaviour
    {
        [SerializeField] private CreationStepDisplay _displayer;
        [SerializeField] private SpecializationCatalog _catalog;
        [SerializeField] private SpecializationButton _buttonPrefab;
        [SerializeField] private Transform _content;

        private Dictionary<SpecializationButton, Character> _buttonsWithPrefabs;
        private SpecializationConfig[] _specializations => _catalog.GetAllSpecs();

        private CharacterCreator _creator;
        private CharacterLoader _loader;
        private GenderPresenter _genderPresenter;

        private Character _character;

        [Inject]
        public void Construct(GenderPresenter presenter, CharacterCreator creator)
        {
            _buttonsWithPrefabs = new Dictionary<SpecializationButton, Character>();
            _loader = new CharacterLoader();

            _creator = creator;
            _genderPresenter = presenter;
            _genderPresenter.Initialized += OnGenderInitialized;
            _genderPresenter.Changed += OnGenderChanged;
        }

        private void OnEnable()
        {
            _displayer.PreviousButtonClicked += ResetAndDelete;
        }

        private void OnGenderInitialized(Character character)
        {
            _character = character;
            Present(_character.Gender);
        }

        private void OnGenderChanged(Character character)
        {
            Destroy(_character.gameObject);
            OnGenderInitialized(character);
        }

        private void Present(Gender gender)
        {
            Reset();
            SpecializationButton selected = null;

            foreach (var specialization in _specializations)
            {
                SpecializationButton specButton = Instantiate(_buttonPrefab, _content);
                specButton.Initialize(specialization);
                specButton.Selected += SpecSelected;

                string prefabPath = gender == Gender.Male ? specialization.MalePrefabPath : specialization.FemalePrefabPath;
                Character characterSpecPrefab = _loader.Load(prefabPath);
                _buttonsWithPrefabs.Add(specButton, characterSpecPrefab);

                if (selected == null)
                    selected = specButton;
            }

            SpecSelected(selected);
        }

        private void SpecSelected(SpecializationButton button)
        {
            Destroy(_character.gameObject);

            foreach (var item in _buttonsWithPrefabs)
            {
                if (button == item.Key)
                {
                    _character = _creator.Create(item.Value);
                }
            }
        }

        private void Reset()
        {
            foreach (var button in _buttonsWithPrefabs)
            {
                button.Key.Selected -= SpecSelected;
                Destroy(button.Key.gameObject);
            }

            _buttonsWithPrefabs.Clear();
        }

        private void ResetAndDelete()
        {
            Reset();
            Destroy(_character.gameObject);
        }

        private void OnDisable()
        {
            Reset();
            _genderPresenter.Initialized -= OnGenderInitialized;
            _genderPresenter.Changed -= OnGenderChanged;
            _displayer.PreviousButtonClicked -= ResetAndDelete;
        }
    }
}