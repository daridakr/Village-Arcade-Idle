using System;

namespace Arena
{
    public interface IHealth
    {
        public event Action<float> Gained;
        public event Action<float> Wasted;
        public event Action Emptied;
    }
}