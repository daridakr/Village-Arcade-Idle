namespace ForeverVillage.Scripts
{
    public class PlayerTimerBuilder : PlayerTimerInteractor
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