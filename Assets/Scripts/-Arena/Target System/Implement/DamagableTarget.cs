using System;
using UnityEngine;

namespace Arena
{
    public class DamagableTarget : Target
    {
        [SerializeField] private Health _health;

        public override event Action<Target> Inactived;

        private void OnEnable()
        {
            _health.Emptied += OnTargetDead;
        }

        private void OnTargetDead()
        {
            _health.Emptied -= OnTargetDead;

            Inactived?.Invoke(this);
        }
    }
}