using UnityEngine;
using Village;
using Village.Player;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private IControlService _controlService;

    protected float _speed;
    private float _speedRate;
    private float _flySpeedRate;

    private Transform _playerModel;
    private PlayerAnimation _animation;
    private Rigidbody _rigidbody;

    public bool IsMoving { get; private set; }

    [Inject]
    private void Construct(IControlService controlService, MovementConfig config)
    {
        _controlService = controlService;
        _controlService.OnMove += Move;
        _controlService.OnStand += Stop;

        _speedRate = config.SpeedRate;
        _flySpeedRate = config.FlySpeedRate;
    }

    public void Setup(Transform model, PlayerAnimation animation)
    {
        _playerModel = model;
        _animation = animation;
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
