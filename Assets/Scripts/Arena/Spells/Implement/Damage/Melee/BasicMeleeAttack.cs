namespace Arena
{
    public class BasicMeleeAttack : DamageSpell
    {
        private readonly BasicMeleeAttackConfig _config;

        public BasicMeleeAttack(BasicMeleeAttackConfig config) : base(config)
        {
            _config = config;
        }

        protected override void Perform(float lifeTime)
        {

        }
    }
}