using UnityEngine;

namespace Arena
{
    [RequireComponent(typeof(Animator))]
    public class MonsterAnimator : MonoBehaviour
    {
        [SerializeField] private MonsterMovement _movementSource;
        [SerializeField] private EnemyHealth _hitSource;
        [SerializeField] private AttackState _attackSource;
        [SerializeField] private VictoryState _victorySoutce;

        private Animator _animator;

        private void OnEnable()
        {
            _movementSource.OnMove += OnMoveAnimation;
            _hitSource.Damaged += PlayHitAnimation;
            _hitSource.Emptied += SetTriggerDeathAnimation;
            _attackSource.OnEnter += StartBasicAttackAnimation;
            _victorySoutce.Winned += PlayVictoryAnimation;
        }

        private void Awake() => _animator = GetComponent<Animator>();

        private void OnMoveAnimation(float velocity) => _animator.SetFloat(AnimationParams.Speed, velocity);
        private void PlayHitAnimation(float _) => _animator.SetTrigger(AnimationParams.Hit);
        private void SetTriggerDeathAnimation() => _animator.SetTrigger(AnimationParams.Death);
        private void StartBasicAttackAnimation() => _animator.SetTrigger(AnimationParams.Attack);
        private void PlayVictoryAnimation() => _animator.SetTrigger(AnimationParams.Victory);

        private void OnDestroy()
        {
            _movementSource.OnMove -= OnMoveAnimation;
            _hitSource.Damaged -= PlayHitAnimation;
            _hitSource.Emptied -= SetTriggerDeathAnimation;
            _attackSource.OnEnter -= StartBasicAttackAnimation;
            _victorySoutce.Winned -= PlayVictoryAnimation;
        }
    }
}