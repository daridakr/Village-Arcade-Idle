using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform _playerModel;
    [SerializeField] private PlayerAnimation _animation;
    [SerializeField] private float _speed;

    private IControlService _controlService;
    private Rigidbody _rigidbody;

    private float _speedRate = 1f;
    private float _flySpeedRate = 1f;

    public bool IsMoving { get; private set; }

    [Inject]
    public void Construct(IControlService controlService)
    {
        _controlService = controlService;

        _controlService.OnMove += Move;
        _controlService.OnStand += Stop;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 direction)
    {
        _playerModel.LookAt(_playerModel.position + direction);
        _rigidbody.velocity = direction * _speed * _speedRate * _flySpeedRate;

        _animation.SetSpeed(direction.magnitude);
        IsMoving = true;
    }

    public void MoveToTarget(Transform target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed);
    }

    public void Stop()
    {
        if (IsMoving)
        {
            if (_rigidbody != null)
            {
                _rigidbody.velocity = Vector3.zero;
            }

            _animation.SetSpeed(0);
            IsMoving = false;
        }
    }

    private void OnDestroy()
    {
        _controlService.OnMove -= Move;
        _controlService.OnStand -= Stop;
    }
}
