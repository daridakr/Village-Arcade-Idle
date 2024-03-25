using UnityEngine;
using Zenject;

namespace Arena
{
    public class EnemyHealth : Health
    {
        [SerializeField] private EnemyHealthConfig _config;

        [Inject]
        private void Construct(PlayerLevelArena arenaLevel) =>
            _config.SetHealthMultiplier(arenaLevel.Level);

        private void Start() => InitPoints(_config);
    }
}