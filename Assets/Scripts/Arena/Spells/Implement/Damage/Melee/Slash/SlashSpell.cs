using UnityEngine;

namespace Arena
{
    public class SlashSpell : DamageSpell
    {
        public float Range => _config.Range;
        public float Speed => _config.Speed;
        public float SlashTime => _config.SlashTime;

        private readonly SlashSpellConfig _config;
        private KnockbackConfig _knockbackConfig => _config.KnockbackConfig;

        public SlashSpell(SlashSpellConfig config) : base(config)
        {
            _config = config;
        }

        protected override void Perform(ITargetsInfo targetsInfo)
        {
            base.Perform(targetsInfo);

            if (_target.TryGetComponent(out IKnockbackable knockbackable))
            {
                Vector3 knockbackForce = (_target.transform.position - _custer.Transform.position).normalized * 50000f;
                knockbackable.Knockback(knockbackForce);
            }
        }
    }
}