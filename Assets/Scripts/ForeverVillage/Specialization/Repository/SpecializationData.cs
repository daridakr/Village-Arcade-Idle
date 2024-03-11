using Arena;
using ForeverVillage;
using System;
using UnityEngine;

namespace Village
{
    [Serializable]
    public class SpecializationData
    {
        [SerializeField] public string Title;
        [SerializeField] public Sprite Icon;
        [SerializeField] public string PrefabPath;
        [SerializeField] public IAvailableWeaponType[] WeaponTypes;
        [SerializeField] public WeaponConfig[] BaseWeapons;
        [SerializeField] public SpellsCatalog Spells;
    }
}