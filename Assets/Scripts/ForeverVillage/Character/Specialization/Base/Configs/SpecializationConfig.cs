using UnityEngine;

namespace ForeverVillage.Scripts.Character
{
    public abstract class SpecializationConfig : ScriptableObject
    {
        [SerializeField] private SpecializationMetadata _metadata;

        public SpecializationMetadata Meta => _metadata;
        public abstract string MalePrefabPath { get; }
        public abstract string FemalePrefabPath { get; }
    }
}