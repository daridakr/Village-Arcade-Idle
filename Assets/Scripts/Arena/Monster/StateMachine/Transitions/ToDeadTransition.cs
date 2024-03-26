using UnityEngine;

namespace Arena
{
    public sealed class ToDeadTransition : StateTransition
    {
        [SerializeField] private EnemyHealth _health;

        protected override void OnEnable() => _health.Emptied += OnDead;

        private void OnDead()
        {
            _health.Emptied -= OnDead;
            NeedTransit = true;
        }
    }
}