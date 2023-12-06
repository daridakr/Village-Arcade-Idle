public class PlayerTimerCleaner : PlayerTimerInteractor
{
    public void StartClean(BuildingZone zone)
    {
        if (zone.State == BuildingZoneState.Destroyed)
        {
            StartInteract(AnimationParams.IsClean);
        }
    }
}
