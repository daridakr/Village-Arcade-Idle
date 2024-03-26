using ForeverVillage;
using UnityEngine;
using Zenject;

namespace Arena
{
    [RequireComponent(typeof(Animator))]
    public sealed class MonsterAnimator : MonoBehaviour
    {
        [SerializeField] private MonsterMovement _movementSource;
        [SerializeField] private EnemyHealth _hitSource;
        [SerializeField] private AttackState _attackSource;
        [SerializeField] private VictoryState _victorySoutce;

        private Animator _animator;
        private ICharacterAnimation _characterAnimation;

        [Inject]
        private void Construct(ICharacterAnimation characterAnimation) => _characterAnimation = characterAnimation;

        private void Awake() => _animator = GetComponent<Animator>();

        private void OnEnable()
        {
            _movementSource.OnMove += SetMoveAnimation;
            _victorySoutce.Winned += PlayVictoryAnimation;

            _hitSource.Damaged += (_) => _characterAnimation.SetHitAnimation(_animator);
            _attackSource.OnEnter += () => _characterAnimation.SetAttackAnimation(_animator);
            _hitSource.Emptied += () => _characterAnimation.SetDeathAnimation(_animator);
        }

        private void SetMoveAnimation(float velocity) => _animator.SetFloat(AnimationParams.Speed, velocity);
        private void PlayVictoryAnimation() => _animator.SetTrigger(AnimationParams.Victory);

        private void OnDestroy()
        {
            _movementSource.OnMove -= SetMoveAnimation;
            _victorySoutce.Winned -= PlayVictoryAnimation;

            _hitSource.Damaged -= (_) => _characterAnimation.SetHitAnimation(_animator);
            _attackSource.OnEnter -= () => _characterAnimation.SetAttackAnimation(_animator);
            _hitSource.Emptied -= () => _characterAnimation.SetDeathAnimation(_animator);
        }
    }
}