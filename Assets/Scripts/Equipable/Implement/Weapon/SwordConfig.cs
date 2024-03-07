using UnityEngine;

namespace ForeverVillage
{
    [CreateAssetMenu(fileName = "NewSwordConfig", menuName = "Item/Equipment/Weapons/Sword")]
    public sealed class SwordConfig : WeaponConfig
    {
        public override Item InstantiateItem()
        {
            return new Sword(this);
        }
    }
}