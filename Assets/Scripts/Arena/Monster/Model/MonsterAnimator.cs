using UnityEngine;

namespace Arena
{
    [RequireComponent(typeof(Animator))]
    public class MonsterAnimator : MonoBehaviour
    {
        [SerializeField] private MonsterMovement _movement;
        [SerializeField] private EnemyHealth _enemyHealth;
        [SerializeField] private AttackState _attackState;

        private Animator _animator;

        private void OnEnable()
        {
            _movement.OnMove += OnMoveAnimation;
            _enemyHealth.Emptied += SetTriggerDeathAnimation;
            _attackState.OnEnter += StartBasicAttackAnimation;
        }

        private void Awake() => _animator = GetComponent<Animator>();

        private void OnMoveAnimation(float velocity) => _animator.SetFloat(AnimationParams.Speed, velocity);
        private void SetTriggerDeathAnimation() => _animator.SetTrigger(AnimationParams.Death);
        private void StartBasicAttackAnimation() => _animator.SetTrigger(AnimationParams.Attack);

        private void OnDestroy()
        {
            _movement.OnMove -= OnMoveAnimation;
            _enemyHealth.Emptied -= SetTriggerDeathAnimation;
            _attackState.OnEnter -= StartBasicAttackAnimation;
        }
    }
}