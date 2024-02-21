using UnityEngine;

[RequireComponent(typeof(Collider))]
public class RepellingCollider : MonoBehaviour
{
    [SerializeField] LayerMask _cullingMask;
    [SerializeField] private Direction _directionType;

    private Collider _collider;
    private Vector3 _direction;

    private const float _repelOffset = 1f;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = false;

        InitDirection();
    }

    private void Start() => CheckForOtherCollidersInside(_collider);

    private void CheckForOtherCollidersInside(Collider collider)
    {
        Vector3 center = _collider.bounds.center;
        float radius = Mathf.Max(_collider.bounds.size.x, _collider.bounds.size.y, _collider.bounds.size.z) / 2f;

        Collider[] collidersInside = Physics.OverlapSphere(center, radius, _cullingMask);

        foreach (Collider other in collidersInside)
        {
            if (other == collider || other.isTrigger)
                continue;

            if (other.bounds.Intersects(collider.bounds))
            {
                other.transform.position += _direction * _repelOffset;
            }
        }
    }

    private void InitDirection()
    {
        switch (_directionType)
        {
            case Direction.UP:
                _direction = Vector3.up;
                break;
            case Direction.DOWN:
                _direction = Vector3.down;
                break;
            case Direction.FORWARD:
                _direction = Vector3.forward;
                break;
            case Direction.BACK:
                _direction = Vector3.back;
                break;
            case Direction.LEFT:
                _direction = Vector3.left;
                break;
            case Direction.RIGHT:
                _direction = Vector3.right;
                break;
            default:
                break;
        }
    }
}
