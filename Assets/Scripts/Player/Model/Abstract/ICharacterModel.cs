using System;
using UnityEngine;

public interface ICharacterModel
{
    public Vector3 LookDirection { get; }
    public Transform CenterTransform { get; }
    public Animator GetAnimator();
    public event Action Initialized;
}
