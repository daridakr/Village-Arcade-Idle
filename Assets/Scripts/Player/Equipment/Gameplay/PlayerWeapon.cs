using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ForeverVillage
{
    public class PlayerWeapon : MonoBehaviour,
        IPlayerWeaponInitializable,
        IPlayerWeaponControl
    {
        private List<Weapon> _weapons;
        private IAvailableWeaponType[] _availibaleTypes;

        private bool CanAdd => _weapons.Count + 1 <= _availibaleTypes.Length;

        public event Action Hidden;
        public event Action Taken;

        public void Init(IAvailableWeaponType[] types) => _availibaleTypes = types;

        private void Start()
        {
            _weapons = new List<Weapon>();

            Take();
        }

        public void Assign(Weapon weapon)
        {
            if (weapon == null)
                return;

            if (CanAdd && _availibaleTypes.Any(availibale => availibale.Type == weapon.Type))
                _weapons.Add(weapon);
        }

        public void Take()
        {
            if (_weapons.Count > 1)
            {
                foreach (Weapon weapon in _weapons)
                    weapon.gameObject.SetActive(true);
            }

            Taken?.Invoke();
        }

        public void Hide()
        {
            if (_weapons.Count > 1)
            {
                foreach (Weapon weapon in _weapons)
                    weapon.gameObject.SetActive(false);
            }

            Hidden?.Invoke();
        }
    }
}