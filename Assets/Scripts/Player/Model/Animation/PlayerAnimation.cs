using UnityEngine;
using Zenject;

public class PlayerAnimation : MonoBehaviour,
    IInitializable
{
    [SerializeField] private PlayerMovement _movement;

    protected IAnimatedModel _model;
    protected Animator _animator;

    private bool _isInitialized = false;

    public virtual void Initialize()
    {
        if (_isInitialized)
            return;

        _model.Initialized += OnModelInitialized;
        _movement.Moving += SetSpeed;

        _isInitialized = true;
    }

    public void InitModel(IAnimatedModel model)
    {
        if (_model != null)
            return;

        _model = model;
    }

    private void SetSpeed(float normalizedSpeed)
    {
        if (_animator)
            _animator.SetFloat(AnimationParams.Speed, normalizedSpeed);
    }

    protected virtual void OnModelInitialized()
    {
        if (_model != null)
            _animator = _model.GetAnimator();
    }

    private void OnDisable()
    {
        _movement.Moving -= SetSpeed;
        _model.Initialized -= OnModelInitialized;
    }
}