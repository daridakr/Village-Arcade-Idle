using UnityEngine;

namespace Arena
{
    public class MonsterLifeTime : MonoBehaviour // temp variant
    {
        [SerializeField] private float _destroyTime;
        [SerializeField] private EnemyHealth _health;
        [SerializeField] private MonsterMovement _movement;

        private void OnEnable() => _health.Emptied += OnMonsterDie;

        private void OnMonsterDie()
        {
            _health.Emptied -= OnMonsterDie;

            _movement.StopMoving();
            Destroy(gameObject, _destroyTime);
        }
    }
}