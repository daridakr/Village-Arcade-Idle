using UnityEngine;
using Vampire;

namespace Arena
{
    public class SlashSpell : DamageSpell
    {
        public float Range => _config.Range;
        public float Speed => _config.Speed;
        public float KnockbackForce => _config.KnockbackForce;

        private readonly SlashSpellConfig _config;

        public SlashSpell(SlashSpellConfig config) : base(config)
        {
            _config = config;
        }

        protected override void Perform(ITargetsInfo targetsInfo)
        {
            base.Perform(targetsInfo);

            if (_target.TryGetComponent(out IKnockbackable knockbackable))
            {
                Vector3 direction = (_custer.Transform.position - _target.transform.position).normalized;
                direction.y = 0;
                knockbackable.Knockback(KnockbackForce, -direction);
            }
        }
    }
}