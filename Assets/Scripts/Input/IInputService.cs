public interface IInputService
{
    public bool IsEnable { get; }

    public void Enable();
    public void Disable();
}