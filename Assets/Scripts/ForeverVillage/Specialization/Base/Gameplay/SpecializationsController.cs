using ForeverVillage.Scripts.Character;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace ForeverVillage.Scripts
{
    public sealed class SpecializationsController : MonoBehaviour,
        ISpecializationsController
    {
        [SerializeField][FormerlySerializedAs("assets")] private SpecializationCatalog _catalog;
        [SerializeField] private Transform _characterPoint;

        private List<Specialization> _specializations;
        private Specialization _current;

        private CharacterLoader _characterLoader;
        private ICustomizableCharacterFactory _characterFactory;
        private CustomizableCharacter _characterInstance;

        public event Action Initialized;

        [Inject]
        public void Construct(ICustomizableCharacterFactory customCharacterFactory)
        {
            _characterFactory = customCharacterFactory;
        }

        private void Awake()
        {
            _characterLoader = new CharacterLoader();
        }

        public void SetupSpecializationsFor(object condition = null)
        {
            _specializations = new List<Specialization>();

            SpecializationConfig[] configs = _catalog.GetAllSpecs();

            foreach (var config in configs)
            {
                var specialization = config.InstantiateSpecialization(condition);
                _specializations.Add(specialization);
            }

            Initialized?.Invoke();
        }

        public MonoBehaviour SelectSpecialization(ISpecialization specialization)
        {
            _current = (Specialization)specialization;
            CustomizableCharacter customizationPrefab = _characterLoader.LoadCustomizable(_current.GetPrefabPath());

            if (_characterInstance != null)
                Destroy(_characterInstance.gameObject);

            _characterInstance = _characterFactory.Create(customizationPrefab, _characterPoint);
            _characterInstance.AddComponent<CharacterTouchRotator>();

            return _characterInstance;
        }

        public ISpecialization[] GetAllSpecializations()
        {
            return _specializations.ToArray();
        }
    }
}