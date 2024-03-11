using System;

namespace Arena
{
    public interface IHealth
    {
        public event Action<float> Changed;
        public event Action Emptied;
    }
}