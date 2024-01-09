using UnityEngine;

namespace ForeverVillage.Scripts
{
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

        public void StartInteract(string animationParamName)
        {
            _animator.SetBool(animationParamName, true);
        }

        public void StopInteract(string animationParamName)
        {
            _animator.SetBool(animationParamName, false);
        }
    }
}