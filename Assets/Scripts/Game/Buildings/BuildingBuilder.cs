public class BuildingBuilder : BuildingInteraction
{
    public void Build(BuildingZone zone)
    {
        if (zone.State == BuildingZoneState.Empty)
        {
            StartInteract(zone, AnimationParams.IsClean);
        }
    }
}
