using UnityEngine;

namespace ForeverVillage
{
    public sealed class Sword : Weapon
    {
        private readonly SwordConfig _config;

        public override GameObject Prefab => _config.Prefab;

        public override WeaponType Type => WeaponType.Sword;
        public override WeaponBodyPart BodyPart => WeaponBodyPart.RightHand;

        public Sword(SwordConfig config) : base(config)
        {
            _config = config;
        }
    }
}