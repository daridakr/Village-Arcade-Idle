namespace ForeverVillage
{
    public sealed class Sword : Weapon
    {
        private SwordConfig _config;

        public Sword(SwordConfig config) : base(config)
        {
            _config = config;
        }
    }
}