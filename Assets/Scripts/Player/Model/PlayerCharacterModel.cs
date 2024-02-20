using System;
using UnityEngine;
using Village.Character;

public class PlayerCharacterModel : MonoBehaviour,
    ICharacterModel, ICustomizableModel, IAnimatedModel
{
    [SerializeField] private PlayerMovement _movement;

    protected CustomizableCharacter _instance;

    private Transform _centerTransform;
    private Vector3 _lookDirection;

    public Vector3 LookDirection => _lookDirection;
    public Transform CenterTransform => _centerTransform;
    public ICustomizableCharacter Customizable => _instance;
    public Transform HeadRig => _instance.HeadRig;
    public Transform HandRig => _instance.HandRig;

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

    protected virtual void UpdateRotation(Vector3 direction)
    {
        _lookDirection = _instance.transform.position + direction;
        _instance.transform.LookAt(_lookDirection);
    }

    protected virtual void OnSetuped()
    {
        _movement.DirectionUpdated += UpdateRotation;
    }

    private void OnDisable()
    {
        _movement.DirectionUpdated -= UpdateRotation;
    }
}