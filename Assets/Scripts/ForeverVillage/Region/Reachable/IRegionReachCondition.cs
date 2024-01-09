using System;

namespace ForeverVillage.Scripts
{
    public interface IRegionReachCondition
    {
        public bool IsCompleted { get; }
        public int Condition { get; }
        public event Action Completed;
    }
}