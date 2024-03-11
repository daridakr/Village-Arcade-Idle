using UnityEngine;

namespace Arena
{
    public class DamagableTarget : Target
    {
        [SerializeField] private Health _helath;

        private void OnEnable()
        {
            _helath.Emptied += OnTargetDeslocated;
        }

        private void OnDisable()
        {
            _helath.Emptied -= OnTargetDeslocated;
        }
    }
}