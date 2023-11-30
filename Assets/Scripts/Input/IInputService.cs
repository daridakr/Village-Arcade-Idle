using System;

public interface IInputService
{
    public bool IsEnable { get; }

    public void Enable();
    public void Disable();
}

public interface IInputReady
{
    public event Action OnEnabled;
    public event Action OnDisabled;
}