using UnityEngine;

public class BuildingBuilder : BuildingInteractor
{
    [SerializeField] private GameObject _hammer;

    public void StartBuildIn(BuildingZone zone)
    {
        if (zone.State == BuildingZoneState.Empty)
        {
            _hammer.SetActive(true);
            StartInteract(zone, AnimationParams.IsBuild);
        }
    }

    protected override void OnCompleted()
    {
        base.OnCompleted();
        _hammer.SetActive(false);
    }
}
