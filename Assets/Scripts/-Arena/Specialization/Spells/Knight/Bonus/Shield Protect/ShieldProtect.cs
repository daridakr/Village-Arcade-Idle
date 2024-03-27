namespace Arena
{
    public sealed class ShieldProtect : BonusSpell
    {
        public ShieldProtect(ShieldProtectConfig config) : base(config)
        {
            //_knockbackForce = config.KnockbackForce;
            //_effect = config.Effect;

            //_damageStrategy = new SingleTargetDamageStrategy(Damage);
        }

        protected override void Perform(ITargetsInfo targetInfo, float additionalDamage)
        {

        }
    }
}