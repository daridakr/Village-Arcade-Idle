namespace Village
{
    public class PlayerTimerBuilder : TimerInteraction
    {
        public void StartBuild(BuildingZone zone)
        {
            if (zone.State == BuildingZoneState.Empty)
            {
                StartInteract(AnimationParams.IsBuild);
            }
        }
    }
}