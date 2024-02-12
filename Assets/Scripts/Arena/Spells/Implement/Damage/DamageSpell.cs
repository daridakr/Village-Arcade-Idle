namespace Arena
{
    public abstract class DamageSpell : Spell
    {
        private readonly DamageSpellConfig _config;

        protected DamageSpell(DamageSpellConfig config) : base(config)
        {
            _config = config;
        }

        protected override void Perform(float lifeTime)
        {

        }
    }
}