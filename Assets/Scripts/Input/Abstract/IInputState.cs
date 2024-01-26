using System;

public interface IInputState
{
    public event Action OnEnabled;
    public event Action OnDisabled;
}