using System;

namespace Village
{
    public abstract class AnimatedTimerInteraction : TimerInteraction,
        IAnimatedInteraction
    {
        protected string _animationParam;

        public string AnimationParam => _animationParam;

        public new event Action<IAnimatedInteraction> Started;

        protected override void OnEnable()
        {
            base.OnEnable();

            InstantiateAnimationParam();
        }

        protected override void StartInteract()
        {
            base.StartInteract();

            Started?.Invoke(this);
        }

        protected abstract void InstantiateAnimationParam();
    }
}