namespace ForeverVillage
{
    public sealed class Sword : Weapon
    {
        private SwordConfig _config;
        public override WeaponType Type => WeaponType.Sword;
        public override WeaponBodyPart BodyPart => WeaponBodyPart.RightHand;

        public Sword(SwordConfig config) : base(config)
        {
            _config = config;
        }
    }
}