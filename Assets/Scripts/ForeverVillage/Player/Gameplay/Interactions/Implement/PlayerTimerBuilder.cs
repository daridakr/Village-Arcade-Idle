namespace Village
{
    public sealed class PlayerTimerBuilder : AnimatedTimerInteraction
    {
        public override string AnimationParam => AnimationParams.Interactions.IsBuild;

        public void StartBuild(BuildingZone zone)
        {
            if (zone.State == BuildingZoneState.Empty)
                StartInteract();
        }
    }
}