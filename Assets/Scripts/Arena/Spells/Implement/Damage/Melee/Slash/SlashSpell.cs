using UnityEngine;

namespace Arena
{
    public class SlashSpell : DamageSpell
    {
        private readonly SlashSpellConfig _config;
        private readonly float _knockbackForce;
        private readonly SpellEffect _effect;

        public SlashSpell(SlashSpellConfig config) : base(config)
        {
            _knockbackForce = config.KnockbackForce;
            _effect = config.Effect;
        }

        protected override void Perform(ITargetsInfo targetsInfo, float additionalDamage)
        {
            base.Perform(targetsInfo, additionalDamage);

            PushWave();

            if (_target.TryGetComponent(out IKnockbackable knockbackable))
                KnockbackTarget(knockbackable);
        }

        private void PushWave()
        {
            SpellEffect spellEffect = Object.Instantiate(_effect, _custer.Transform.position, _effect.transform.rotation);
            spellEffect.SetTarget(_target.transform);
        }

        private void KnockbackTarget(IKnockbackable knockbackable)
        {
            Vector3 direction = (_custer.Transform.position - _target.transform.position).normalized;
            direction.y = 0;
            knockbackable.Knockback(_knockbackForce, -direction);
        }
    }
}