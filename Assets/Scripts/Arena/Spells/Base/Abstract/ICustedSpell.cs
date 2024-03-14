using System;

namespace Arena
{
    public interface ICustedSpell
    {
        public void StartCusting(ICusterPosition custer, ITargetsInfo targets);
        public event Action Custed;
    }
}