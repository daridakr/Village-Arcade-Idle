using System;
using UnityEngine;
using Zenject;

namespace Arena
{
    public sealed class ChaseAIState : AIState
    {
        [SerializeField, Min(0f)] private float _stoppingDistance = 1.5f;
        [SerializeField, Min(0f)] private float _pathUpdateTickTime = 0.25f;

        private Transform _target;

        public event Action<float> OnEnter;
        public event Action OnExit;
        public override event Action<Vector3> Updated;
        public override event Action<AIState[]> CanTransit;

        [Inject]
        private void Construct(PlayerMovement player) => _target = player.transform;

        public override float GetWeight() => 100f * base.GetWeight();

        public override void OnEnterState(AIState previousState)
        {
            InvokeRepeating(nameof(UpdatePathToTarget), UnityEngine.Random.Range(0f, _pathUpdateTickTime), _pathUpdateTickTime);
            OnEnter?.Invoke(_stoppingDistance);
            UpdatePathToTarget();
        }

        private void UpdatePathToTarget()
        {
            if (Vector3.Distance(transform.position, _target.position) <= _stoppingDistance)
            {
                CanTransit?.Invoke(new AIState[] { this });
                return;
            }

            Updated?.Invoke(_target.position);
        }

        public override void OnExitState(AIState nextState)
        {
            CancelInvoke(nameof(UpdatePathToTarget));
            OnExit?.Invoke();
        }

        public override bool CanEnterState() => Vector3.Distance(transform.position, _target.position) > _stoppingDistance;
        public override bool CanExitState() => true;
    }
}