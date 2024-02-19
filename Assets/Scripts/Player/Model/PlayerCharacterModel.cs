using System;
using UnityEngine;
using Village.Character;

public class PlayerCharacterModel : MonoBehaviour,
    ICharacterModel, ICustomizableModel
{
    protected CustomizableCharacter _instance;

    private Transform _centerTransform;
    private Vector3 _lookDirection;

    public Vector3 LookDirection => _lookDirection;
    public Transform CenterTransform => _centerTransform;
    public CustomizableCharacter Character => _instance;

    public event Action Initialized;

    public void Create(CustomizableCharacter prefab)
    {
        if (_instance != null)
            return;

        _instance = Instantiate(prefab, transform);
        _centerTransform = _instance.HeadRig;

        OnSetuped();

        Initialized?.Invoke();
    }

    public Animator GetAnimator()
    {
        return _instance.Animator;
    }

    public virtual void UpdateRotation(Vector3 direction)
    {
        _lookDirection = _instance.transform.position + direction;
        _instance.transform.LookAt(_lookDirection);
    }

    protected virtual void OnSetuped() { }
}