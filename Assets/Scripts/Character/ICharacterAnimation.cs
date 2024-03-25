using UnityEngine;

namespace ForeverVillage
{
    public interface ICharacterAnimation
    {
        public void SetAttackAnimation(Animator animator);
        public void SetDeathAnimation(Animator animator);
        public void SetHitAnimation(Animator animator);
    }
}