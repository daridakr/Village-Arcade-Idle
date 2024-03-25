using UnityEngine;

namespace Arena
{
    public class MonsterAttacker : MonoBehaviour,
        ITransformCuster
    {
        [SerializeField] private AttackState _attackState;

        public Transform Transform => transform;

        private void OnEnable() => _attackState.Attacked += AttackTarget;

        private void AttackTarget()
        {

        }

        private void OnDestroy() => _attackState.Attacked -= AttackTarget;
    }
}