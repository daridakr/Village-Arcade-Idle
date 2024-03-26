namespace Village
{
    public sealed class PlayerTimerCleaner : AnimatedTimerInteraction
    {
        public override string AnimationParam => AnimationParams.Interactions.IsClean;

        public void StartClean(BuildingZone zone)
        {
            if (zone.State == BuildingZoneState.Destroyed)
                StartInteract();
        }
    }
}