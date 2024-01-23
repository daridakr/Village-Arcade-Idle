using UnityEngine;

namespace ForeverVillage.Scripts
{
    public class PlayerTimerCleaner : PlayerTimerInteractor
    {
        [SerializeField] private GameObject _shovel;

        public void StartClean(BuildingZone zone)
        {
            if (zone.State == BuildingZoneState.Destroyed)
            {
                _shovel.SetActive(true);
                StartInteract(AnimationParams.IsClean);
            }
        }

        protected override void OnCompleted()
        {
            base.OnCompleted();
            _shovel.SetActive(false);
        }
    }
}