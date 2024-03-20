using System;
using UnityEngine;

public interface IControlService
{
    public bool InControl { get; }

    public event Action<Vector3> OnControl;
    public event Action OnExit;
}