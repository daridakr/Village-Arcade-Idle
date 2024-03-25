namespace Arena
{
    public sealed class SingleTargetDamageStrategy : DamageStrategy
    {
        private Target _target;

        public Target Target => _target;

        public SingleTargetDamageStrategy(float damage) : base(damage) { }

        public override void DealDamage(ITargetsInfo targetsInfo, float additionalDamage)
        {
            _target = targetsInfo.Nearest;

            if (_target.TryGetComponent(out IDamagable damagable))
                damagable.TakeDamage(_damage + additionalDamage);
        }
    }
}