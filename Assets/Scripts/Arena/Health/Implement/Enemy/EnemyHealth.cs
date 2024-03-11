using UnityEngine;

namespace Arena
{
    public class EnemyHealth : Health
    {
        [SerializeField] private EnemyHealthConfig _config;

        private void Awake() => InitPoints(_config);

        public override void TakeDamage(float damage)
        {
            // отскок монстров при получении урона

            base.TakeDamage(damage);
        }
    }
}