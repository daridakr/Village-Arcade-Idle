using System;
using UnityEngine;
using Zenject;

public sealed class JoystickInputControl : 
    IInputService, IInputState, IControlService,
    IInitializable, ITickable, IDisposable
{
    private Joystick _joystick;

    public bool IsEnable { get; private set; }

    public event Action OnEnabled;
    public event Action OnDisabled;

    public bool InControl => _joystick.Direction != Vector2.zero;

    public event Action<Vector3> OnMove;
    public event Action OnStand;

    [Inject]
    private void Construct(Joystick joystick) => _joystick = joystick;

    public void Initialize()
    {
        Enable();
    }

    public void Tick()
    {
        if (IsEnable)
        {
            HandleInputToMove();
        }
    }

    private void HandleInputToMove()
    {
        if (InControl)
        {
            Vector3 rawDirection = new Vector3(_joystick.Direction.x, 0, _joystick.Direction.y);
            OnMove?.Invoke(rawDirection);
        }
        else
        {
            OnStand?.Invoke();
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
        OnStand?.Invoke();
    }

    public void Dispose()
    {
        Disable();
    }
}