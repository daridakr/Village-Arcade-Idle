using UnityEngine;

namespace Arena
{
    [CreateAssetMenu(fileName = "NewSpellsCatalog", menuName = "Spells/Catalog")]
    public sealed class SpellsCatalog : ScriptableObject
    {
        [SerializeField] private SpellConfig[] _data;

        public SpellConfig[] GetAllSpells() => _data;
    }
}