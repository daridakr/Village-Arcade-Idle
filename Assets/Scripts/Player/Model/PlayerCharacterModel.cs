using System;
using UnityEngine;
using Village.Character;
using Zenject;

public class PlayerCharacterModel : MonoBehaviour,
    ICharacterModel, ICustomizableModel, IAnimatedModel
{
    [SerializeField] private IControlService _playerControl;

    protected CustomizableCharacter _instance;
    private Transform _centerTransform;
    private Vector3 _lookDirection;
    private bool _isMovementDirection;

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

    [Inject]
    public void Construct(IControlService playerControl) => _playerControl = playerControl;

    public void Create(CustomizableCharacter prefab)
    {
        if (_instance != null)
            return;

        _instance = Instantiate(prefab, transform);
        _centerTransform = _instance.HeadRig;

        StartListenMovementDirection();

        Initialized?.Invoke();
    }

    public Animator GetAnimator()
    {
        return _instance.Animator;
    }

    protected void UpdateRotation(Vector3 direction)
    {
        //_lookDirection = _instance.transform.position + direction;
        //_instance.transform.LookAt(_lookDirection);
    }

    protected void StartListenMovementDirection()
    {
        if (!_isMovementDirection)
        {
            _playerControl.OnMove += UpdateRotation;
            _isMovementDirection = true;
        }
    }

    protected void StopListenMovementDirection()
    {
        _playerControl.OnMove -= UpdateRotation;
        _isMovementDirection = false;
    }

    private void OnDestroy() => StopListenMovementDirection();
}