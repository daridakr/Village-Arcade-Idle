using System;

namespace Village
{
    public abstract class AnimatedTimerInteraction : TimerInteraction,
        IAnimatedInteraction
    {
        public abstract string AnimationParam { get; }

        public new event Action<IAnimatedInteraction> Started;

        protected override void StartInteract()
        {
            base.StartInteract();

            Started?.Invoke(this);
        }
    }
}