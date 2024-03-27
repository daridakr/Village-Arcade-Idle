namespace Arena
{
    public sealed class MonsterBite : DamageSpell
    {
        private readonly SingleTargetDamageStrategy _damageStrategy;

        protected override IDamageStrategy Strategy => _damageStrategy;

        public MonsterBite(MonsterBiteConfig config) : base(config)
        {
            _damageStrategy = new SingleTargetDamageStrategy(Damage);
        }
    }
}