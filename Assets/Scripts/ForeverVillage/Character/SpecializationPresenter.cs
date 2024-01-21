using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace ForeverVillage.Scripts.Character
{
    public sealed class SpecializationPresenter : MonoBehaviour
    {
        [SerializeField] private CreationStepDisplay _displayer;
        [SerializeField] private SpecializationCatalog _specCatalog;
        [SerializeField] private SpecializationButtonView _specButtonPrefab;
        [SerializeField] private SpecializationInfoDisplayer _specInfoDisplayer;
        [SerializeField] private Transform _content;
        [SerializeField] private Transform _characterPoint;

        private Dictionary<SpecializationButtonView, CustomizableCharacter> _specButtonsWithPrefabs;
        private SpecializationConfig[] _specializations => _specCatalog.GetAllSpecs();

        private ICustomizableCharacterFactory _customCharacterFactory;
        private CharacterLoader _loader;
        private GenderPresenter _genderPresenter;

        private CustomizableCharacter _character;
        private SpecializationButtonView _selected;

        public event Action<CustomizableCharacter> Selected;

        [Inject]
        public void Construct(ICustomizableCharacterFactory characterFactory, GenderPresenter presenter)
        {
            _specButtonsWithPrefabs = new Dictionary<SpecializationButtonView, CustomizableCharacter>();
            _loader = new CharacterLoader();

            _customCharacterFactory = characterFactory;
            _genderPresenter = presenter;
            _genderPresenter.Changed += Present;
        }

        private void OnEnable() => _displayer.PreviousButtonClicked += ResetAndDelete;

        private void Present(Gender gender)
        {
            Reset();
            SpecializationButtonView @default = null;

            foreach (var specialization in _specializations)
            {
                SpecializationButtonView specButton = Instantiate(_specButtonPrefab, _content);
                specButton.Initialize(specialization.Meta);
                specButton.Selected += SpecSelected;

                string prefabPath = gender == Gender.Male ? specialization.MalePrefabPath : specialization.FemalePrefabPath;
                CustomizableCharacter characterSpecPrefab = _loader.LoadCustomizable(prefabPath);
                _specButtonsWithPrefabs.Add(specButton, characterSpecPrefab);

                if (@default == null)
                    @default = specButton;
            }

            SpecSelected(@default);
        }

        private void SpecSelected(SpecializationButtonView button)
        {
            if (_selected != null)
                _selected.Unselect();

            button.Select();
            _selected = button;

            if (_character != null)
                Destroy(_character.gameObject);

            foreach (var item in _specButtonsWithPrefabs)
            {
                if (button == item.Key)
                {
                    // _buildingFactory.Create(_building, _buildPoint);
                    _character = _customCharacterFactory.Create(item.Value, _characterPoint);
                    _character.AddComponent<CharacterTouchRotator>();
                    Selected?.Invoke(_character.GetComponent<CustomizableCharacter>());

                    _specInfoDisplayer.Display(item.Key.Info);
                }
            }
        }

        private void Reset()
        {
            foreach (var button in _specButtonsWithPrefabs)
            {
                button.Key.Selected -= SpecSelected;
                Destroy(button.Key.gameObject);
            }

            _specButtonsWithPrefabs.Clear();
        }

        private void ResetAndDelete()
        {
            Reset();
            Destroy(_character.gameObject);
        }

        private void OnDisable()
        {
            Reset();
            _genderPresenter.Changed -= Present;
            _displayer.PreviousButtonClicked -= ResetAndDelete;
        }
    }
}