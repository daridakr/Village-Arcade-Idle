using UnityEngine;

namespace Village
{
    [CreateAssetMenu(fileName = "NewSpecializationCatalog", menuName = "Character/Specialization Catalog")]
    public sealed class SpecializationCatalog : ScriptableObject
    {
        [SerializeField] private SpecializationConfig[] _data;

        public SpecializationConfig[] GetAllSpecs()
        {
            return _data;
        }
    }
}