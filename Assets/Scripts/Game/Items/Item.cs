using UnityEngine;

[RequireComponent (typeof(Collider))]
public class Item : MonoBehaviour
{
    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    public void DisableCollision()
    {
        _collider.enabled = false;
    }
}
