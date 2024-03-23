namespace Arena
{
    public abstract class DamageSpell : Spell
    {
        private readonly DamageSpellConfig _config;
        protected Target _target;

        public float Damage => _config.Damage;

        protected DamageSpell(DamageSpellConfig config) : base(config)
        {
            _config = config;
        }

        protected override void Perform(ITargetsInfo targetsInfo) // melee
        {
            _target = targetsInfo.Nearest;

            if (_target.TryGetComponent(out IDamagable damagable))
                damagable.TakeDamage(Damage);
        }
    }
}