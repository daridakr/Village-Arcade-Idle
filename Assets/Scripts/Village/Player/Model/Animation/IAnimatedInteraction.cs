using System;

namespace Village
{
    public interface IAnimatedInteraction : IInteraction
    {
        public string AnimationParam { get; }

        public new event Action<IAnimatedInteraction> Started;
    }
}