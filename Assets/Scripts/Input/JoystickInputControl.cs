using System;
using UnityEngine;

public class JoystickInputControl : MonoBehaviour, IInputService, IInputReady, IControlService
{
    [SerializeField] private Joystick _joystick;

    public bool IsEnable { get; private set; }

    public event Action OnEnabled;
    public event Action OnDisabled;

    public bool InControl => _joystick.Direction != Vector2.zero;

    public event Action<Vector3> OnMove;
    public event Action OnStand;

    private void Start()
    {
        Enable();
    }

    public void FixedUpdate()
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

    private void OnDisable()
    {
        Disable();
    }
}
