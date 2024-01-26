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
        private Specialization _selected;
        private CustomizableCharacter _characterInstance;
        private SpecializationInstantiator _instantiator;

        public event Action Initialized;

        [Inject]
        private void Construct(SpecializationInstantiator instantiator) => _instantiator = instantiator;

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
            _selected = (Specialization)specialization;

            if (_characterInstance != null)
                Destroy(_characterInstance.gameObject);

            _characterInstance = _instantiator.Instantiate(_selected, _characterPoint);
            _characterInstance.AddComponent<CharacterTouchRotator>();

            return _characterInstance;
        }

        public ISpecialization[] GetAllSpecializations()
        {
            return _specializations.ToArray();
        }

        public ISpecialization GetSelectedSpecialization()
        {
            return _selected;
        }
    }
}