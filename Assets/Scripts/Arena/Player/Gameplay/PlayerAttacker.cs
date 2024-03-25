using System;
using UnityEngine;

namespace Arena
{
    public sealed class PlayerAttacker : MonoBehaviour
    {
        [SerializeField] private RangeTargetDetector _targetDetector;
        [SerializeField] private PlayerSpellsCuster _spells;
        [SerializeField] private PlayerWeaponArena _weapon;

        private float _totalDamage;
        private Spell _mainSpell;
        private bool _isAttacking;

        public event Action Attacked;

        private void OnEnable()
        {
            //_targetDetector.Changed += OnTargetChanged;
            _targetDetector.OnNoneTarget += StopAttack;
            _spells.Initialized += OnSpellsInitialized;
        }

        private void OnSpellsInitialized()
        {
            _spells.Initialized -= OnSpellsInitialized;

            _mainSpell = _spells.GetMain();
        }

        private void Update()
        {
            if (_targetDetector.IsTargetDetected && !_isAttacking)
            {
                _mainSpell.Custed += () => Attacked?.Invoke();
                _isAttacking = true;
            }

            if (_isAttacking)
                _spells.Cust(_targetDetector, _weapon);
        }

        private void StopAttack()
        {
            if (_isAttacking)
            {
                _mainSpell.Custed -= () => Attacked?.Invoke();
                _isAttacking = false;
            }
        }

        private void OnDisable()
        {
            _targetDetector.OnNoneTarget -= StopAttack;
            _mainSpell.Custed -= () => Attacked?.Invoke();
        }
    }
}