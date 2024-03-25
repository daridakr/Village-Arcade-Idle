using UnityEngine;

namespace Arena
{
    public class MonsterAttacker : MonoBehaviour,
        ITransformCuster
    {
        [SerializeField] private AttackState _attackState;
        [SerializeField] private DamageSpellConfig _config;

        private ITargetsInfo _currentTargets;
        private Spell _spell;

        public Transform Transform => transform;

        private void Awake() => _spell = _config.InstantiateSpell();

        private void OnEnable()
        {
            _attackState.Attacked += SetAttackingTargetInfo;
            _attackState.OnExit += ResetTargetInfo;
        }    

        private void Update()
        {
            if (_currentTargets == null || _spell == null)
                return;

            _spell.StartCusting(this, _currentTargets);
        }

        private void SetAttackingTargetInfo(ITargetsInfo target) => _currentTargets = target;
        private void ResetTargetInfo() => _currentTargets = null;

        private void OnDestroy()
        {
            _attackState.Attacked -= SetAttackingTargetInfo;
            _attackState.OnExit -= ResetTargetInfo;
        }
    }
}