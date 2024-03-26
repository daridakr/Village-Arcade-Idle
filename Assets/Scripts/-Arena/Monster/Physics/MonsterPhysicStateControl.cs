using UnityEngine;
using UnityEngine.AI;

namespace Arena
{
    [RequireComponent(typeof(Rigidbody), (typeof(NavMeshAgent)))]
    public sealed class MonsterPhysicStateControl : MonoBehaviour
    {
        [SerializeField] private KnockbackableEntity _knockable;

        private Rigidbody _body;
        private NavMeshAgent _meshAgent;

        private void Awake()
        {
            _body = GetComponent<Rigidbody>();
            _meshAgent = GetComponent<NavMeshAgent>();
        }

        private void OnEnable()
        {
            _knockable.OnEnter += EnablePhysics;
            _knockable.OnExit += DisablePhysics;
        }

        private void EnablePhysics()
        {
            _meshAgent.enabled = false;
            _body.isKinematic = false;
        }

        private void DisablePhysics()
        {
            _body.isKinematic = true;
            _meshAgent.enabled = true;
        }

        private void OnDestroy()
        {
            _knockable.OnEnter -= EnablePhysics;
            _knockable.OnExit -= DisablePhysics;
        }
    }
}