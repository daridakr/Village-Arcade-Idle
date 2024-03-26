using UnityEngine;

namespace Arena
{
    [CreateAssetMenu(fileName = "EnemyHealthConfig", menuName = "Health/Enemy Health Config")]
    public class EnemyHealthConfig : HealthConfig
    {
        private int _multiplier = 1;

        public void SetHealthMultiplier(int multiplier)
        {
            if (multiplier > 0)
                _multiplier = multiplier;
        }

        public override float Max => base.Max * _multiplier;
    }
}