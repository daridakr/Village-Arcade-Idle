namespace Village
{
    public sealed class PlayerTimerBuilder : AnimatedTimerInteraction
    {
        public void StartBuild(BuildingZone zone)
        {
            if (zone.State == BuildingZoneState.Empty)
            {
                StartInteract();
            }
        }

        protected override void InstantiateAnimationParam()
        {
            _animationParam = AnimationParams.Interactions.IsBuild;
        }
    }
}