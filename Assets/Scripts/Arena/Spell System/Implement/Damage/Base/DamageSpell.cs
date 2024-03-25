namespace Arena
{
    public abstract class DamageSpell : Spell
    {
        private readonly DamageSpellConfig _config;

        protected abstract IDamageStrategy Strategy { get; }

        public float Damage => _config.Damage;

        protected DamageSpell(DamageSpellConfig config) : base(config) =>
            _config = config;

        protected override void Perform(ITargetsInfo targetsInfo, float additionalDamage) =>
            Strategy.DealDamage(targetsInfo, additionalDamage);
    }
}