using System;
using UnityEngine;

namespace ForeverVillage
{
    [Serializable]
    public class ItemPerk :
        IItemPerk
    {
        [SerializeField] private PerkType _type;
        [SerializeField] private float _value;

        public PerkType Type => _type;
        public float Value => _value;
    }
}