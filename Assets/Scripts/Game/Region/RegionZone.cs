using UnityEngine;

[RequireComponent (typeof(BoxCollider))]
public class RegionZone : MonoBehaviour
{
    private BoxCollider _collider;

    private void OnDrawGizmos()
    {
        _collider = GetComponent<BoxCollider>();
        Gizmos.color = Color.red;
        Matrix4x4 rotationMatrix = Matrix4x4.TRS(_collider.transform.position, _collider.transform.rotation, _collider.transform.lossyScale);
        Gizmos.matrix = rotationMatrix;
        Gizmos.DrawWireCube(_collider.center, _collider.size);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerInteractor player))
        {
            
        }
    }
}
