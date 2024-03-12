using UnityEngine;

namespace Arena
{
    public class DamagableTarget : Target
    {
        [SerializeField] private Health _health;

        private void OnEnable()
        {
            _health.Emptied += OnTargetDeslocated;
        }

        private void OnDisable()
        {
            _health.Emptied -= OnTargetDeslocated;
        }
    }
}