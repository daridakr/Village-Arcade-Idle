using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetSpeed(float normalizedSpeed)
    {
        if (_animator)
        {
            _animator.SetFloat(AnimationParams.Speed, normalizedSpeed);
        }
    }

    public void StartCleaning()
    {
        _animator.SetBool(AnimationParams.IsClean, true);
    }

    public void StopCleaning()
    {
        _animator.SetBool(AnimationParams.IsClean, false);
    }

    private static class AnimationParams
    {
        public static readonly string Speed = nameof(Speed);
        public static readonly string Idle = nameof(Idle);
        public static readonly string Flying = nameof(Flying);
        public static readonly string IsClean = nameof(IsClean);
    }
}
