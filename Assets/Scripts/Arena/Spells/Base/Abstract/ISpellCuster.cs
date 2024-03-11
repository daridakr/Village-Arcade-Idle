using System;

namespace Arena
{
    public interface ISpellCuster
    {
        //public void Cust(ITargetable[] targets);
        public event Action Custed;
    }
}