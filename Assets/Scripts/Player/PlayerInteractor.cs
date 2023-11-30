using UnityEngine;
using Zenject;

[RequireComponent (typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerInteractor : MonoBehaviour
{
    private Collider _collider;
    private Rigidbody _body;
    private IInputReady _inputReady;

    [Inject]
    public void Construct(IInputReady inputReady)
    {
        _inputReady = inputReady;

        _inputReady.OnEnabled += EnableInteraction;
        _inputReady.OnDisabled += DisableInteraction;
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
        _inputReady.OnEnabled -= EnableInteraction;
        _inputReady.OnDisabled -= DisableInteraction;
    }
}
