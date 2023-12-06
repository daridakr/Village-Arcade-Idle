using UnityEngine;

public class PlayerTimerBuilder : PlayerTimerInteractor
{
    [SerializeField] private GameObject _hammer;

    public void StartBuild(BuildingZone zone)
    {
        if (zone.State == BuildingZoneState.Empty)
        {
            _hammer.SetActive(true);
            StartInteract(AnimationParams.IsBuild);
        }
    }

    protected override void OnCompleted()
    {
        base.OnCompleted();
        _hammer.SetActive(false);
    }
}
