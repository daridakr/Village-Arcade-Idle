using System;
using UnityEngine;
using Village.Player;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    protected Rigidbody _rigidbody;
    protected float _speed;
    protected float _maxRotationSpeed;
    protected bool _isMovingRotation = true;

    private IControlService _controlService;

    public bool IsMoving { get; private set; }
    public Vector3 CurrentPosition => transform.position;

    public event Action<float> Moving;

    [Inject]
    private void Construct(IControlService controlService, MovementConfig config)
    {
        _controlService = controlService;
        _controlService.OnControl += OnControlled;
        _controlService.OnExit += OnExitControl;

        _speed = config.Speed;
        _maxRotationSpeed = config.MaxRotationSpeed;
    }

    private void Awake() => _rigidbody = GetComponent<Rigidbody>();

    private void OnControlled(Vector3 direction)
    {
        if (direction == Vector3.zero)
            return;

        Move(direction);

        if (_isMovingRotation)
            UpdateRotation(direction);

        Moving?.Invoke(direction.magnitude);
        IsMoving = true;
    }

    private void Move(Vector3 moveDirection)
    {
        Vector3 velocity = moveDirection * _speed * Time.fixedDeltaTime;
        _rigidbody.velocity = velocity;
    }

    protected void UpdateRotation(Vector3 direction)
    {
        float angularMagnitude = GetAngularMagnitude(direction);
        _rigidbody.angularVelocity = new Vector3(0, angularMagnitude, 0);
    }

    private void OnExitControl()
    {
        if (IsMoving)
        {
            if (_rigidbody == null)
                return;

            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;

            Moving?.Invoke(0);
            IsMoving = false;
        }
    }

    private float GetAngularMagnitude(Vector3 direction)
    {
        Vector3 directionNormalized = direction.normalized;

        float currentRotation = _rigidbody.rotation.eulerAngles.y * Mathf.Deg2Rad;
        float targetRotation = Mathf.Atan2(directionNormalized.x, directionNormalized.z);
        float delta = Mathf.DeltaAngle(currentRotation * Mathf.Rad2Deg, targetRotation * Mathf.Rad2Deg) * Mathf.Deg2Rad;

        return Mathf.Clamp(delta / Time.fixedDeltaTime, -_maxRotationSpeed, _maxRotationSpeed);
    }

    private void OnDestroy()
    {
        _controlService.OnControl -= OnControlled;
        _controlService.OnExit -= OnExitControl;

        OnDestroyed();
    }

    protected virtual void OnDestroyed() { }
}