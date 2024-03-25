using UnityEngine;

namespace Arena
{
    public sealed class SwordStroke : DamageSpell
    {
        private readonly float _knockbackForce;
        private readonly SpellEffect _effect;
        private readonly SingleTargetDamageStrategy _damageStrategy;

        protected override IDamageStrategy Strategy => _damageStrategy;

        public SwordStroke(SwordStrokeConfig config) : base(config)
        {
            _knockbackForce = config.KnockbackForce;
            _effect = config.Effect;

            _damageStrategy = new SingleTargetDamageStrategy(Damage);
        }

        protected override void Perform(ITargetsInfo targetsInfo, float additionalDamage)
        {
            base.Perform(targetsInfo, additionalDamage);

            PushSlashEffect();

            if (_damageStrategy.Target.TryGetComponent(out IKnockbackable knockbackable))
                KnockbackTarget(knockbackable);
        }

        private void PushSlashEffect()
        {
            SpellEffect spellEffect = Object.Instantiate(_effect, _custer.Transform.position, _effect.transform.rotation);
            spellEffect.SetTarget(_damageStrategy.Target.transform);
        }

        private void KnockbackTarget(IKnockbackable knockbackable)
        {
            Vector3 direction = (_custer.Transform.position - _damageStrategy.Target.transform.position).normalized;
            direction.y = 0;
            knockbackable.Knockback(_knockbackForce, -direction);
        }
    }
}