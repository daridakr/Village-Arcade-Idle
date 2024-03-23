using System;

namespace Arena
{
    public interface ICustedSpell
    {
        public void StartCusting(ICusterPosition custer, ITargetsInfo targets, float additionalDamage = 0);
        public event Action Custed;
    }
}