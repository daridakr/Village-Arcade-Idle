using UnityEngine;

[RequireComponent (typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerInteraction : MonoBehaviour
{
    private Collider _collider;
    private Rigidbody _body;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _body = GetComponent<Rigidbody>();
    }

    public void EnableInteraction()
    {
        _collider.enabled = true;
        _body.isKinematic = false;
    }

    public void DisableInteraction()
    {
        _collider.enabled = false;
        _body.isKinematic = true;
    }
}
