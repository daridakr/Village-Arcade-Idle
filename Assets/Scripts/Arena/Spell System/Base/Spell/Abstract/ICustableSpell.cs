using System;

namespace Arena
{
    public interface ICustableSpell
    {
        public void StartCusting(ITransformCuster custer, ITargetsInfo targets, float additionalDamage = 0);
        public event Action Custed;
    }
}