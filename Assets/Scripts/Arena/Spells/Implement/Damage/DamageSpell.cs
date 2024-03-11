namespace Arena
{
    public abstract class DamageSpell : Spell
    {
        private readonly DamageSpellConfig _config;

        public float Damage => _config.Damage;
        public float VOffset => _config.VerticalOffset;

        protected DamageSpell(DamageSpellConfig config) : base(config)
        {
            _config = config;
        }

        protected override void Perform(ITargetsInfo targetsInfo)
        {
            Target nearest = targetsInfo.Nearest;

            if (nearest.TryGetComponent(out IDamagable damagable))
                damagable.TakeDamage(Damage);
        }
    }
}