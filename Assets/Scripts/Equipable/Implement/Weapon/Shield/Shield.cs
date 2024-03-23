using UnityEngine;

namespace ForeverVillage
{
    public sealed class Shield : Weapon
    {
        private readonly ShieldConfig _config;

        public override GameObject Prefab => _config.Prefab;

        public override WeaponType Type => WeaponType.Shield;
        public override WeaponBodyPart BodyPart => WeaponBodyPart.LeftHand;

        public Shield(ShieldConfig config) : base(config)
        {
            _config = config;
        }
    }
}
