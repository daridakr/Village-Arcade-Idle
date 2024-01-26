using UnityEngine;
using Zenject;

namespace ForeverVillage.Scripts
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerInteractor : MonoBehaviour
    {
        private Collider _collider;
        private Rigidbody _body;
        private IInputState _inputState;

        [Inject]
        private void Construct(IInputState inputState)
        {
            _inputState = inputState;

            _inputState.OnEnabled += EnableInteraction;
            _inputState.OnDisabled += DisableInteraction;
        }

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _body = GetComponent<Rigidbody>();
        }

        private void EnableInteraction()
        {
            _collider.enabled = true;
            _body.isKinematic = false;
        }

        private void DisableInteraction()
        {
            _collider.enabled = false;
            _body.isKinematic = true;
        }

        private void OnDestroy()
        {
            _inputState.OnEnabled -= EnableInteraction;
            _inputState.OnDisabled -= DisableInteraction;
        }
    }
}