using UnityEngine;
using Zenject;

namespace Village
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public sealed class PlayerPhysicStateControl : MonoBehaviour
    {
        private Collider _collider;
        private Rigidbody _body;
        private IInputState _inputState;

        [Inject]
        private void Construct(IInputState inputState)
        {
            _inputState = inputState;

            _inputState.OnEnabled += EnablePhysics;
            _inputState.OnDisabled += DisablePhysics;
        }

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _body = GetComponent<Rigidbody>();
        }

        private void EnablePhysics()
        {
            _collider.enabled = true;
            _body.isKinematic = false;
        }

        private void DisablePhysics()
        {
            _collider.enabled = false;
            _body.isKinematic = true;
        }

        private void OnDestroy()
        {
            _inputState.OnEnabled -= EnablePhysics;
            _inputState.OnDisabled -= DisablePhysics;
        }
    }
}