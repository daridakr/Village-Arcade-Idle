using ForeverVillage;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour,
    IPlayerWeaponInitializable,
    IPlayerWeaponControl
{
    protected Dictionary<Weapon, GameObject> _weapons;
    private IAvailableWeaponType[] _availibaleTypes;

    private bool CanAdd => _weapons.Count + 1 <= _availibaleTypes.Length;

    public event Action Hidden;
    public event Action Taken;

    public void Init(IAvailableWeaponType[] types) => _availibaleTypes = types;

    private void Start()
    {
        _weapons = new Dictionary<Weapon, GameObject>();

        Take();
    }

    public void Assign(Weapon weapon, GameObject instance)
    {
        if (weapon == null)
            return;

        if (CanAdd && _availibaleTypes.Any(availibale => availibale.Type == weapon.Type))
            _weapons.Add(weapon, instance);
    }

    public void Take()
    {
        if (_weapons.Count > 1)
        {
            foreach (GameObject weapon in _weapons.Values)
                weapon.SetActive(true);
        }

        Taken?.Invoke();
    }

    public void Hide()
    {
        if (_weapons.Count > 1)
        {
            foreach (GameObject weapon in _weapons.Values)
                weapon.SetActive(false);
        }

        Hidden?.Invoke();
    }
}
