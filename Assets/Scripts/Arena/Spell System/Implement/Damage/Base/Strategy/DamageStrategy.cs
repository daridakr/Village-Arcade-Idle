namespace Arena
{
    public abstract class DamageStrategy :
        IDamageStrategy
    {
        protected float _damage;

        public DamageStrategy(float damage) => _damage = damage;

        public abstract void DealDamage(ITargetsInfo targetsInfo, float additionalDamage);
    }
}