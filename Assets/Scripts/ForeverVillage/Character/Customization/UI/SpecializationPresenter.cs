using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ForeverVillage.Scripts.Character
{
    public sealed class SpecializationPresenter : MonoBehaviour
    {
        [SerializeField] private CreationStepDisplay _displayer;
        [SerializeField] private SpecializationCatalog _specCatalog;
        [SerializeField] private SpecializationButton _specButtonPrefab;
        [SerializeField] private SpecializationInfoDisplayer _specInfoDisplayer;
        [SerializeField] private Transform _content;

        private Dictionary<SpecializationButton, Character> _specButtonsWithPrefabs;
        private SpecializationConfig[] _specializations => _specCatalog.GetAllSpecs();

        private CharacterCreator _creator;
        private CharacterLoader _loader;
        private GenderPresenter _genderPresenter;

        private Character _character;
        private SpecializationButton _selected;

        [Inject]
        public void Construct(CharacterCreator creator, GenderPresenter presenter)
        {
            _specButtonsWithPrefabs = new Dictionary<SpecializationButton, Character>();
            _loader = new CharacterLoader();

            _creator = creator;

            _genderPresenter = presenter;
            _genderPresenter.Changed += Present;
        }

        private void OnEnable() => _displayer.PreviousButtonClicked += ResetAndDelete;

        private void Present(Gender gender)
        {
            Reset();
            SpecializationButton @default = null;

            foreach (var specialization in _specializations)
            {
                SpecializationButton specButton = Instantiate(_specButtonPrefab, _content);
                specButton.Initialize(specialization.Meta);
                specButton.Selected += SpecSelected;

                string prefabPath = gender == Gender.Male ? specialization.MalePrefabPath : specialization.FemalePrefabPath;
                Character characterSpecPrefab = _loader.Load(prefabPath);
                _specButtonsWithPrefabs.Add(specButton, characterSpecPrefab);

                if (@default == null)
                    @default = specButton;
            }

            SpecSelected(@default);
        }

        private void SpecSelected(SpecializationButton button)
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
                    _character = _creator.Create(item.Value);
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