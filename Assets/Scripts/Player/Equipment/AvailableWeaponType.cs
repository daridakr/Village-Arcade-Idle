using System;
using UnityEngine;

namespace ForeverVillage
{
    [Serializable]
    public class AvailableWeaponType :
        IAvailableWeaponType
    {
        [SerializeField] private WeaponType _type;
        [SerializeField] private float _count;

        public WeaponType Type => _type;
        public float Count => _count;
    }
}