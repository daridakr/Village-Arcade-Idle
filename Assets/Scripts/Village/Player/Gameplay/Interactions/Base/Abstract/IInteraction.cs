using System;

namespace Village
{
    public interface IInteraction
    {
        public event Action Started;
        public event Action Stopped;
    }
}