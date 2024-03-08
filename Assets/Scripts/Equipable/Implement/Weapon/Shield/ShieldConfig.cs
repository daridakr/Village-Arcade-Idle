using UnityEngine;

namespace ForeverVillage
{
    [CreateAssetMenu(fileName = "NewShieldConfig", menuName = "Item/Equipment/Weapons/Shield")]
    public sealed class ShieldConfig : WeaponConfig
    {
        public override Item InstantiateItem() => new Shield(this);
    }
}