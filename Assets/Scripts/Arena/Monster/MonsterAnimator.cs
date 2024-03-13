using UnityEngine;

namespace Arena
{
    [RequireComponent(typeof(Animator))]
    public class MonsterAnimator : MonoBehaviour
    {
        [SerializeField] private MonsterMovement _movement;

        private Animator _animator;

        private void OnEnable()
        {
            _movement.OnMove += SetMovement;
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void SetMovement(bool value)
        {
            _animator.SetBool(AnimationParams.Monster.IsWalking, value);
        }

        private void OnDisable()
        {
            _movement.OnMove -= SetMovement;
        }
    }
}