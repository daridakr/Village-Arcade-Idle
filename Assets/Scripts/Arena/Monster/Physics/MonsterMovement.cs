using System;
using UnityEngine;
using UnityEngine.AI;

namespace Arena
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class MonsterMovement : KnockbackableEntity
    {
        [SerializeField] private ChaseState _chaseState;

        private NavMeshAgent _meshAgent;
        private bool IsEnabled => _meshAgent.enabled;

        public event Action<float> OnMove;

        protected override void Awake()
        {
            base.Awake();

            _meshAgent = GetComponent<NavMeshAgent>();
        }

        private void OnEnable() => _chaseState.Updated += UpdateDestination;

        protected override void FixedUpdate()
        {
            if (IsEnabled)
                OnMove?.Invoke(_meshAgent.velocity.magnitude);

            base.FixedUpdate();
        }

        private void UpdateDestination(Vector3 target)
        {
            if (IsEnabled)
                _meshAgent.SetDestination(target);
        }

        private void OnDestroy() => _chaseState.Updated -= UpdateDestination;
    }
}