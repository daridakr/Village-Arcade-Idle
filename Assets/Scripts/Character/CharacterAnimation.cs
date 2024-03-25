using UnityEngine;

namespace ForeverVillage
{
    public class CharacterAnimation :
        ICharacterAnimation
    {
        public void SetAttackAnimation(Animator animator) => animator.SetTrigger(AnimationParams.Attack);
        public void SetDeathAnimation(Animator animator) => animator.SetTrigger(AnimationParams.Death);
        public void SetHitAnimation(Animator animator) => animator.SetTrigger(AnimationParams.Hit);
    }
}