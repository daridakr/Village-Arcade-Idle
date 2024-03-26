using System;
using UnityEngine;
using Zenject;

namespace Arena
{
    public class EnemyHealth : Health
    {
        [SerializeField] private EnemyHealthConfig _config;

        public event Action<EnemyHealth> Dead;

        [Inject]
        private void Construct(PlayerLevelArena arenaLevel) =>
            _config.SetHealthMultiplier(arenaLevel.Level);

        private void OnEnable() => Emptied += OnEnemyDead;
        private void Start() => InitPoints(_config);

        private void OnEnemyDead()
        {
            Emptied -= OnEnemyDead;
            Dead?.Invoke(this);
        }
    }
}