using Zenject;

namespace Village
{
    public class JoystickView : CanvasView
    {
        private IInputState _inputReady;

        [Inject]
        private void Construct(IInputState inputReady)
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
}