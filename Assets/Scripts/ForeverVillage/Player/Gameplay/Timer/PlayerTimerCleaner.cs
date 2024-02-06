namespace Village
{
    public class PlayerTimerCleaner : TimerInteraction
    {
        public void StartClean(BuildingZone zone)
        {
            if (zone.State == BuildingZoneState.Destroyed)
            {
                StartInteract(AnimationParams.IsClean);
            }
        }
    }
}