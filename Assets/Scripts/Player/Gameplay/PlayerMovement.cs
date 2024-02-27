using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Village.Player;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private SurfaceSlider _slider;

    protected Rigidbody _rigidbody;
    protected float _speed;
    private float _speedRate;
    private float _flySpeedRate;

    private IControlService _controlService;

    public bool IsMoving { get; private set; }
    public Vector3 CurrentPosition => transform.position;

    public event Action<float> OnMove;
    public event Action<Vector3> DirectionUpdated;

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
        _rigidbody.freezeRotation = true;
    }

    protected virtual void Move(Vector3 direction)
    {
        OnMove?.Invoke(direction.magnitude);

        direction *= _speed * Time.deltaTime;
        _rigidbody.MovePosition(_rigidbody.position + direction);

        DirectionUpdated?.Invoke(direction);

        IsMoving = true;
    }

    private void Stop()
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