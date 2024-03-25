using UnityEngine;

namespace Arena
{
    public class ArenaPlayerAnimation : PlayerAnimation
    {
        [SerializeField] private PlayerAttacker _attacker;
        [SerializeField] private PlayerHealth _health;

        private void OnEnable()
        {
            _attacker.Attacked += PlayAttackAnimation;
            _health.Damaged += PlayHitAnimation;
        }

        private void PlayAttackAnimation() => _animator.SetTrigger(AnimationParams.Attack);
        private void PlayHitAnimation(float _) => _animator.SetTrigger(AnimationParams.Hit);

        protected override void OnDisable()
        {
            base.OnDisable();

            _attacker.Attacked -= PlayAttackAnimation;
            _health.Damaged -= PlayHitAnimation;
        }
    }
}