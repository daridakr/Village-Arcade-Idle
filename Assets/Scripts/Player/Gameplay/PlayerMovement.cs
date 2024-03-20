using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Village.Player;
using Zenject;
using static Cinemachine.CinemachineOrbitalTransposer;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    protected Rigidbody _rigidbody;
    protected float _speed;

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
    }

    private void Awake() => _rigidbody = GetComponent<Rigidbody>();

    [SerializeField] private float maxRotationSpeed = 360f;

    protected virtual void Move(Vector3 direction)
    {
        OnMove?.Invoke(direction.magnitude);

        if (direction != Vector3.zero)
        {
            Vector3 velocity = direction * _speed * Time.fixedDeltaTime;
            _rigidbody.velocity = velocity;

            float currentHeading = _rigidbody.rotation.eulerAngles.y * Mathf.Deg2Rad;
            float targetHeading = Mathf.Atan2(direction.normalized.x, direction.normalized.z);
            float deltaHeading = Mathf.DeltaAngle(currentHeading * Mathf.Rad2Deg, targetHeading * Mathf.Rad2Deg) * Mathf.Deg2Rad;

            float angularVelocityMagnitude = Mathf.Clamp(deltaHeading / Time.fixedDeltaTime, -maxRotationSpeed, maxRotationSpeed);
            _rigidbody.angularVelocity = new Vector3(0, angularVelocityMagnitude, 0);
        }

        IsMoving = true;
    }

    private void Stop()
    {
        if (IsMoving)
        {
            if (_rigidbody != null)
            {
                _rigidbody.velocity = Vector3.zero;
                _rigidbody.angularVelocity = Vector3.zero;
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