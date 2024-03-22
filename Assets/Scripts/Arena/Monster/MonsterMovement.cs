using System;
using UnityEngine;
using UnityEngine.AI;

namespace Arena
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class MonsterMovement : KnockbackableEntity
    {
        [SerializeField] private ChaseAIState _chaseState;

        private NavMeshAgent _meshAgent;
        private bool IsEnabled => _meshAgent.enabled;

        public event Action<float> OnMove;

        private void OnEnable()
        {
            _meshAgent = GetComponent<NavMeshAgent>();

            _chaseState.OnEnter += OnStartChase;
            _chaseState.Updated += UpdateDestination;
            _chaseState.OnExit += OnStopChase;
        }

        protected override void FixedUpdate()
        {
            if (IsEnabled)
                OnMove?.Invoke(_meshAgent.velocity.magnitude);

            base.FixedUpdate();
        }

        private void OnStartChase(float stoppingDistance)
        {
            if (IsEnabled)
                _meshAgent.stoppingDistance = stoppingDistance;
        }


        private void UpdateDestination(Vector3 target)
        {
            if (IsEnabled)
                _meshAgent.SetDestination(target);
        }

        private void OnStopChase()
        {
            if (IsEnabled)
                _meshAgent.ResetPath();
        }

        public override void Knockback(float force, Vector3 direction)
        {
            _meshAgent.enabled = false;

            base.Knockback(force, direction);
        }

        protected override void OnKnockedback() => _meshAgent.enabled = true;

        private void OnDestroy()
        {
            _chaseState.OnEnter -= OnStartChase;
            _chaseState.Updated -= UpdateDestination;
            _chaseState.OnExit -= OnStopChase;
        }
    }
}