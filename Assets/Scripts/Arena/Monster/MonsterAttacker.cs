using UnityEngine;

namespace Arena
{
    public class MonsterAttacker : MonoBehaviour,
        ITransformCuster
    {
        [SerializeField] private AttackState _attackState;
        [SerializeField] private DamageSpell _spell;

        private ITargetsInfo _currentTargets;

        public Transform Transform => transform;

        private void OnEnable() => _attackState.Attacked += AttackTarget;

        private void AttackTarget(ITargetsInfo target)
        {
            _currentTargets = target;
        }

        private void Update()
        {
            if (_currentTargets == null)
                return;

            _spell.StartCusting(this, _currentTargets);
        }

        private void OnDestroy() => _attackState.Attacked -= AttackTarget;
    }
}