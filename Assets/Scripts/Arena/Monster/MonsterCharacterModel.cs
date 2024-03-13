using UnityEngine;
using UnityEngine.AI;

namespace Arena
{
    public class MonsterCharacterModel : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;

        private LookAtIK _lookAt;

        private void Awake() => _lookAt = GetComponent<LookAtIK>();

        private void Update()
        {
            if (_lookAt != null)
                _lookAt.lookAtTargetPosition = _agent.steeringTarget + transform.forward;
        }
    }
}