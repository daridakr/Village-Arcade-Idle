using System;
using UnityEngine;

public class JoystickInput : MonoBehaviour
{
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private Joystick _joystick;

    public bool Moving => _joystick.Direction != Vector2.zero;

    public event Action Moved;

    public void FixedUpdate()
    {
        if (!Moving)
        {
            _movement.Stop();
            return;
        }

        Vector3 rawDirection = new Vector3(_joystick.Direction.x, 0, _joystick.Direction.y);
        _movement.Move(rawDirection);

        Moved?.Invoke();
    }

    private void OnDisable()
    {
        _movement?.Stop();
    }
}
