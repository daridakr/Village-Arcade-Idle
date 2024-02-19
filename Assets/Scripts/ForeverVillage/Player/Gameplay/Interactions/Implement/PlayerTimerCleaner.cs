namespace Village
{
    public sealed class PlayerTimerCleaner : AnimatedTimerInteraction
    {
        public void StartClean(BuildingZone zone)
        {
            if (zone.State == BuildingZoneState.Destroyed)
            {
                StartInteract();
            }
        }

        protected override void InstantiateAnimationParam()
        {
            _animationParam = AnimationParams.Interactions.IsClean;
        }
    }
}