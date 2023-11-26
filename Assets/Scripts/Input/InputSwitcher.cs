using UnityEngine;

public class InputSwitcher : MonoBehaviour
{
    [SerializeField] private JoystickInput _joystickInput;
    [SerializeField] private Canvas _joystickCanvas;
    [SerializeField] private PlayerInteraction _playerInteraction;

    // disable player move.
    public void Disable()
    {
        _joystickInput.enabled = false;
        _joystickCanvas.enabled = false;
        _playerInteraction.DisableInteraction();
    }

    // enable player move.
    public void Enable()
    {
        _joystickInput.enabled = true;
        _joystickCanvas.enabled = true;
        _playerInteraction.EnableInteraction();
    }
}
