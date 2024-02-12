using System;

namespace Arena
{
    public interface ISpellAction
    {
        public void Cust(ITargetable target);
        public event Action Custed;
    }

    public interface ITargetable
    {

    }
}