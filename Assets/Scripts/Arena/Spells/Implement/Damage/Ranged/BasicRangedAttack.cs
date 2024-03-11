namespace Arena
{
    public class BasicRangedAttack : DamageSpell
    {
        private readonly BasicRangedAttackConfig _config;

        public BasicRangedAttack(BasicRangedAttackConfig config) : base(config)
        {
            _config = config;
        }
    }
}