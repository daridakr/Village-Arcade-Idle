using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Arena
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof (Rigidbody))]
    public sealed class MonsterKnockback : MonoBehaviour
    {
        [SerializeField] private EnemyHealth _health;
        [SerializeField] private float _distance = 3f;
        [SerializeField] private float _force = 10f;

        private PlayerMovement _playerMovement;

        private NavMeshAgent _agent;
        private Rigidbody _body;

        [Inject]
        private void Construct(PlayerMovement playerMovement) => _playerMovement = playerMovement;

        private void OnEnable() => _health.Damaged += Knockback;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _body = GetComponent<Rigidbody>();
        }

        private void Knockback(float damage)
        {
            _agent.enabled = false;
            _body.useGravity = true;
            _body.isKinematic = false;

            Vector3 directionFromPlayer = transform.position - _playerMovement.CurrentPosition;
            directionFromPlayer.Normalize();

            _body.AddForce(directionFromPlayer * _force, ForceMode.Impulse);
        }

        private void OnDisable() => _health.Damaged -= Knockback;

        public void Knockback(System.Numerics.Vector3 force)
        {
            throw new System.NotImplementedException();
        }
    }
}