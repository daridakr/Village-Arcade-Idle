using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Village.Character;
using Zenject;

namespace Village
{
    public sealed class SpecializationsController : MonoBehaviour,
        ISpecializationsController,
        ISpecializationGetter
    {
        [SerializeField][FormerlySerializedAs("assets")] private SpecializationCatalog _catalog;
        [SerializeField] private Transform _characterPoint;

        private List<ICharacterSpecialization> _specializations;
        private ICharacterSpecialization _selected;

        private CustomizableCharacter _characterInstance;
        private SpecializationInstantiator _instantiator;

        public ICharacterSpecialization Selected => _selected;

        public event Action Initialized;

        [Inject]
        private void Construct(SpecializationInstantiator instantiator) => _instantiator = instantiator;

        public void SetupSpecializationsFor(object condition = null)
        {
            _specializations = new List<ICharacterSpecialization>();

            SpecializationConfig[] configs = _catalog.GetAllSpecs();

            foreach (var config in configs)
            {
                ICharacterSpecialization characterSpecialization = config.InstantiateSpecialization(condition) as ICharacterSpecialization;
                _specializations.Add(characterSpecialization);
            }

            Initialized?.Invoke();
        }

        public ICustomizableCharacter SelectSpecialization(ICharacterSpecialization specialization)
        {
            _selected = specialization;

            if (_characterInstance != null)
                Destroy(_characterInstance.gameObject);

            _characterInstance = _instantiator.Instantiate(specialization, _characterPoint);
            _characterInstance.AddComponent<CharacterTouchRotator>();

            return _characterInstance;
        }

        public ICharacterSpecialization[] GetAllSpecializations() => _specializations.ToArray();

        public ICharacterSpecialization GetSpecialization(string guid)
        {
            SpecializationConfig[] configs = _catalog.GetAllSpecs();

            foreach (var config in configs)
            {
                if (config.Id == guid)
                    return (ICharacterSpecialization)config.InstantiateSpecialization();
            }

            return null;
        }
    }
}