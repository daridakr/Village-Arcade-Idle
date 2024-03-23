using UnityEngine;

namespace ForeverVillage
{
    public sealed class PlayerEquipment : MonoBehaviour,
        IPlayerWeaponEquipment
    {
        [SerializeField] private PlayerCharacterModel _model;
        [SerializeField] private PlayerWeapon _weapon;

        private IArmor[] _armor;

        public void EquipWeapon(Weapon weapon)
        {
            Transform weaponRig =
                weapon.BodyPart == WeaponBodyPart.RightHand ?
                _model.HandRigRight : _model.HandRigLeft;

            GameObject equiped = weapon.Equip(weaponRig);
            _weapon.Assign(weapon, equiped);
        }

        //private void EquipWeapon(Weapon weapon)
        //{
        //    // подходит ли он по специализации, по другим параметрам

        //    //_weapon = _equiper.Equip(weapon);

        //    //weapon.Unquip();
        //    weapon.Equip(_model.HandRigR);
        //}
    }
}