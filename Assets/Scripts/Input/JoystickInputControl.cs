using System;
using UnityEngine;
using Zenject;

public sealed class JoystickInputControl : MonoBehaviour,
    IInputService, IInputState, IControlService
{
    private Joystick _joystick;

    public bool IsEnable { get; private set; }

    public event Action OnEnabled;
    public event Action OnDisabled;

    public bool InControl => _joystick.Direction != Vector2.zero;

    public event Action<Vector3> OnControl;
    public event Action OnExit;

    [Inject]
    private void Construct(Joystick joystick) => _joystick = joystick;

    private void Start() => Enable();

    private void FixedUpdate()
    {
        if (IsEnable)
            HandleInputToMove();
    }

    private void HandleInputToMove()
    {
        if (InControl)
        {
            Vector3 rawDirection = new Vector3(_joystick.Direction.x, 0, _joystick.Direction.y);
            OnControl?.Invoke(rawDirection);
        }
        else
        {
            OnExit?.Invoke();
            return;
        }
    }

    public void Enable()
    {
        IsEnable = true;
        OnEnabled?.Invoke();
    }

    public void Disable()
    {
        IsEnable = false;
        OnDisabled?.Invoke();
        OnExit?.Invoke();
    }

    private void OnDisable() => Disable();
}