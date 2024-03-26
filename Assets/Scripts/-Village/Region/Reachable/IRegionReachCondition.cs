using System;

namespace Village
{
    public interface IRegionReachCondition
    {
        public bool IsCompleted { get; }
        public int Condition { get; }
        public event Action Completed;
    }
}