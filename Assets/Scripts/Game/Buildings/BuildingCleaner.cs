public class BuildingCleaner : BuildingInteractor
{
    public void Clean(BuildingZone zone)
    {
        if (zone.State == BuildingZoneState.Destroyed)
        {
            StartInteract(zone, AnimationParams.IsClean);
        }
    }
}
