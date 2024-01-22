using UnityEngine;

namespace ForeverVillage.Scripts
{
    public abstract class SpecializationConfig : ScriptableObject
    {
        [SerializeField] private SpecializationMetadata _metadata;

        public SpecializationMetadata Meta => _metadata;

        public abstract Specialization InstantiateSpecialization(object condition = null);
    }
}