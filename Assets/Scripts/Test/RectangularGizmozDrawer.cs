using UnityEngine;

public class RectangularGizmozDrawer : MonoBehaviour
{
    [SerializeField] private Vector2 _size;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, new Vector3(_size.x, 1, _size.y));
    }
}
