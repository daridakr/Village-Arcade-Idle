using UnityEngine;

public interface ICharacterModel : IInitilizable
{
    public Vector3 LookDirection { get; }
    public Transform CenterTransform { get; }
}