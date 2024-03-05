using UnityEngine;

namespace Arena
{
    public class PlayerEquipment : MonoBehaviour
    {
        [SerializeField] private PlayerCharacterModel _model;

        private Equiper _equiper;

        private IEquippable _weapon;

        private void Awake()
        {
            _equiper = new Equiper();
        }

        private void EquipWeapon(Weapon weapon)
        {
            // подходит ли он по специализации, по другим параметрам

            //_weapon = _equiper.Equip(weapon);

            //weapon.Unquip();
            weapon.Equip(_model.HandRigR);
        }
    }


    public class Equiper
    {
        public void Equip(IEquippable equippable)
        {

        }
    }
}