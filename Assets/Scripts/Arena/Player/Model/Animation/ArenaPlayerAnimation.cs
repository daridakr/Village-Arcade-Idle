using ForeverVillage;
using UnityEngine;
using Zenject;

namespace Arena
{
    public class ArenaPlayerAnimation : PlayerAnimation
    {
        [SerializeField] private PlayerAttacker _attackSource;
        [SerializeField] private PlayerHealth _hitSource;

        private ICharacterAnimation _characterAnimation;

        [Inject]
        private void Construct(ICharacterAnimation characterAnimation) => _characterAnimation = characterAnimation;

        private void OnEnable()
        {
            _hitSource.Damaged += (_) => _characterAnimation.SetHitAnimation(_animator);
            _attackSource.Attacked += () => _characterAnimation.SetAttackAnimation(_animator);
            _hitSource.Emptied += () => _characterAnimation.SetDeathAnimation(_animator);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _hitSource.Damaged -= (_) => _characterAnimation.SetHitAnimation(_animator);
            _attackSource.Attacked -= () => _characterAnimation.SetAttackAnimation(_animator);
            _hitSource.Emptied -= () => _characterAnimation.SetDeathAnimation(_animator);
        }
    }
}