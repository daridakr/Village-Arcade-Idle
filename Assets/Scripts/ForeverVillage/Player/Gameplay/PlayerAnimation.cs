using UnityEngine;
using Zenject;

public class PlayerAnimation : MonoBehaviour,
    IInitializable
{
    [SerializeField] protected PlayerCharacterModel _model;
    [SerializeField] private PlayerMovement _movement;

    protected Animator _animator;

    public void Initialize()
    {
        _model.Initialized += OnModelInitialized;
        _movement.OnMove += SetSpeed;
    }

    public void SetSpeed(float normalizedSpeed)
    {
        if (_animator)
            _animator.SetFloat(AnimationParams.Speed, normalizedSpeed);
    }

    protected virtual void OnModelInitialized()
    {
        _animator = _model.GetAnimator();
    }

    private void OnDisable()
    {
        _movement.OnMove -= SetSpeed;
        _model.Initialized -= OnModelInitialized;
    }
}