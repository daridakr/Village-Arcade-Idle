using UnityEngine;
using Zenject;

public class InputControlSceneDisabler : MonoBehaviour
{
    private IInputService _inputService;

    [Inject]
    private void Construct(IInputService input)
    {
        _inputService = input;
    }

    private void Start()
    {
        _inputService.Disable();
    }

    private void OnDisable()
    {
        _inputService.Enable();
    }
}
