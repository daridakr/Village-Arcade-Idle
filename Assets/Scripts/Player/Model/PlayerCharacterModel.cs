using System;
using UnityEngine;
using Village.Character;

public class PlayerCharacterModel : MonoBehaviour,
    ICharacterModel, ICustomizableModel, IAnimatedModel
{
    protected CustomizableCharacter _instance;
    private Transform _centerTransform;
    private Vector3 _lookDirection;

    #region API
    public Vector3 LookDirection => _lookDirection;
    public Vector3 Position => transform.position;
    public Transform CenterTransform => _centerTransform;
    public ICustomizableCharacter Customizable => _instance;
    public Transform HeadRig => _instance.HeadRig;
    public Transform HandRigLeft => _instance.HandRigLeft;
    public Transform HandRigRight => _instance.HandRigRight;
    #endregion

    public event Action Initialized;

    public void Create(CustomizableCharacter prefab)
    {
        if (_instance != null)
            return;

        _instance = Instantiate(prefab, transform);
        _centerTransform = _instance.HeadRig;

        Initialized?.Invoke();
    }

    public Animator GetAnimator() => _instance.Animator;
}