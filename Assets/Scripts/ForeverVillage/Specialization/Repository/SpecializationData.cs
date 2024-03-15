using Arena;
using System;
using UnityEngine;

namespace Village
{
    [Serializable]
    public class SpecializationData
    {
        [SerializeField] public string Id;
        [SerializeField] public string PrefabPath;
        [SerializeField] public SpellsCatalog Spells;
    }
}