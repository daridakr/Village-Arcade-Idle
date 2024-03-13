using UnityEngine;

namespace Arena
{
    public class ArenaPlayerAnimation : PlayerAnimation
    {
        [SerializeField] private PlayerAttacker _attacker;

        private void OnEnable() => _attacker.Attacked += PlayAttackAnimation;

        private void PlayAttackAnimation() => _animator.SetTrigger(AnimationParams.Attack);

        private void OnDisable() => _attacker.Attacked -= PlayAttackAnimation;
    }
}