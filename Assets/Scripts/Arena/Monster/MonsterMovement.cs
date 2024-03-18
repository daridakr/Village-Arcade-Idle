using System;
using UnityEngine;
using UnityEngine.AI;

namespace Arena
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class MonsterMovement : MonoBehaviour,
        IKnockbackable
    {
        [SerializeField] private ChaseAIState _chaseState;
        [SerializeField] private float _maxKnockbackTime = 0.5f;

        private NavMeshAgent _meshAgent;

        public event Action<float> OnMove;

        private void OnEnable()
        {
            _meshAgent = GetComponent<NavMeshAgent>();

            _chaseState.OnEnter += OnChaseStateEnter;
            _chaseState.Updated += UpdateDestination;
            _chaseState.OnExit += OnChaseStateExit;
        }

        private void Update() => OnMove?.Invoke(_meshAgent.velocity.magnitude);

        private void OnChaseStateEnter(float stoppingDistance) => _meshAgent.stoppingDistance = stoppingDistance;
        private void UpdateDestination(Vector3 target) => _meshAgent.SetDestination(target);
        private void OnChaseStateExit() => _meshAgent.ResetPath();

        public void Knockback(Vector3 force)
        {
            //if (_physicsControl.IsAgentEnabled)
            //{
            //    StopCoroutine(_moveCoroutine);
            //    _moveCoroutine = StartCoroutine(ApplyKnockback(force));
            //}
        }

        //private IEnumerator ApplyKnockback(Vector3 force)
        //{
        //    yield return null;

        //    _physicsControl.EnableBody();
        //    _physicsControl.AddBodyForce(force);

        //    yield return new WaitForFixedUpdate();
        //    float knockbackTime = Time.time;

        //    yield return new WaitUntil(
        //        () => _physicsControl.BodyVelocityMagnitude < _stillThreshold || Time.time > knockbackTime + _maxKnockbackTime
        //    );

        //    yield return new WaitForSeconds(0.25f);

        //    _physicsControl.DisableBody();
        //    _physicsControl.AgentWarp(transform.position);

        //    yield return null;

        //    if (_playerPosition != null)
        //        _moveCoroutine = StartCoroutine(ChasePlayer(_playerPosition));
        //    else
        //        _moveCoroutine = StartCoroutine(Roam());
        //}

        private void OnDestroy()
        {
            _chaseState.OnEnter -= OnChaseStateEnter;
            _chaseState.Updated -= UpdateDestination;
            _chaseState.OnExit -= OnChaseStateExit;
        }
    }
}