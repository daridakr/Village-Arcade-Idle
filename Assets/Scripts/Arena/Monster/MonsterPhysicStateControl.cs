using UnityEngine;
using UnityEngine.AI;

namespace Arena
{
    [RequireComponent(typeof(Collider), (typeof(Rigidbody)),(typeof(NavMeshAgent)))]
    public class MonsterPhysicStateControl : MonoBehaviour
    {
        private Collider _collider;
        private Rigidbody _body;
        private NavMeshAgent _agent;

        public float AgentVelocityMagnitude => _agent.velocity.magnitude;
        public float BodyVelocityMagnitude => _body.velocity.magnitude;
        public float RemainingDistance => _agent.remainingDistance;
        public float StoppingDistance => _agent.stoppingDistance;
        public bool IsAgentEnabled => _agent.enabled;

        public void Initialize()
        {
            _collider = GetComponent<Collider>();
            _body = GetComponent<Rigidbody>();
            _agent = GetComponent<NavMeshAgent>();
        }

        public void EnablePhysics()
        {
            _collider.enabled = true;
            EnableAgent();
            _body.isKinematic = false;
        }

        public void DisablePhysics()
        {
            _collider.enabled = false;
            DisableAgent();
            _body.isKinematic = true;
        }

        public void EnableBody()
        {
            _body.useGravity = true;
            _body.isKinematic = false;
        }

        public void DisableBody()
        {
            _body.velocity = Vector3.zero;
            _body.angularVelocity = Vector3.zero;
            _body.useGravity = false;
            _body.isKinematic = true;
        }

        public void EnableAgent() => _agent.enabled = true;
        public void DisableAgent() => _agent.enabled = false;
        public void StopAgent() => _agent.isStopped = true;

        public bool SetAgentDestination(Vector3 target) => _agent.SetDestination(target);
        public void SetAgentStoppingDistance(float distance) => _agent.stoppingDistance = distance;
        public void ResetAgentPath() => _agent.ResetPath();
        public void AgentWarp(Vector3 newPosition) => _agent.Warp(newPosition);
        public void AddBodyForce(Vector3 force) => _body.AddForce(force);
    }
}