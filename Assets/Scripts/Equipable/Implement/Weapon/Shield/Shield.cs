namespace ForeverVillage
{
    public sealed class Shield : Weapon
    {
        private ShieldConfig _config;
        public override WeaponType Type => WeaponType.Shield;
        public override WeaponBodyPart BodyPart => WeaponBodyPart.LeftHand;

        public Shield(ShieldConfig config) : base(config)
        {
            _config = config;
        }
    }
}
