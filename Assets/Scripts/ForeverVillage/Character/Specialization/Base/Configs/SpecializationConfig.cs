using UnityEngine;

namespace ForeverVillage.Scripts.Character
{
    public abstract class SpecializationConfig : ScriptableObject
    {
        [SerializeField] private SpecializationMetadata _metadata;

        public string Title => _metadata.Title;
        public string Description => _metadata.Description;
        public Sprite Icon => _metadata.Icon;
        public abstract string MalePrefabPath { get; }
        public abstract string FemalePrefabPath { get; }
    }
}