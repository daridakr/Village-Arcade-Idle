using ForeverVillage;
using UnityEngine;

namespace Arena
{
    public class PlayerEquipment : MonoBehaviour
    {
        [SerializeField] private PlayerCharacterModel _model;

        private Weapon _activeWeapon;
        private IArmor[] _armor;

        public void EquipWeapon(IWeapon weapon)
        {
            _activeWeapon = weapon.Equip(_model.HandRigR) as Weapon;
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