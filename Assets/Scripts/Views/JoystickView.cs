using Zenject;

public class JoystickView : CanvasView
{
    private IInputReady _inputReady;

    [Inject]
    public void Construct(IInputReady inputReady)
    {
        _inputReady = inputReady;

        _inputReady.OnEnabled += Display;
        _inputReady.OnDisabled += Hide;
    }

    private void OnDisable()
    {
        _inputReady.OnEnabled -= Display;
        _inputReady.OnDisabled -= Hide;
    }
}
