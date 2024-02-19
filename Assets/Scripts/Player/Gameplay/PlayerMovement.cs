using System;
using UnityEngine;
using Village.Player;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerCharacterModel _playerModel;

    protected Rigidbody _rigidbody;
    protected float _speed;
    private float _speedRate;
    private float _flySpeedRate;

    private IControlService _controlService;

    public bool IsMoving { get; private set; }
    public Vector3 CurrentPosition => transform.position;

    public event Action<float> OnMove;

    [Inject]
    private void Construct(IControlService controlService, MovementConfig config)
    {
        _controlService = controlService;
        _controlService.OnMove += Move;
        _controlService.OnStand += Stop;

        _speed = config.Speed;
        _speedRate = config.SpeedRate;
        _flySpeedRate = config.FlySpeedRate;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 direction)
    {
        _playerModel?.UpdateRotation(direction);
        _rigidbody.velocity = direction * _speed * _speedRate * _flySpeedRate;

        OnMove?.Invoke(direction.magnitude);
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

            OnMove?.Invoke(0);
            IsMoving = false;
        }
    }

    private void OnDestroy()
    {
        _controlService.OnMove -= Move;
        _controlService.OnStand -= Stop;

        OnDestroyed();
    }

    protected virtual void OnDestroyed() { }
}