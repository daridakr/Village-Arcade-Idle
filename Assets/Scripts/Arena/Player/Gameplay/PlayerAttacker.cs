using ForeverVillage;
using System;
using UnityEngine;

namespace Arena
{
    public sealed class PlayerAttacker : MonoBehaviour
    {
        [SerializeField] private TargetDetector _targetDetector;
        [SerializeField] private PlayerSpells _spells;
        [SerializeField] private PlayerWeapon _weapon;

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
            {
                // в общий дамаг плюсуется дамаг от оружия с дамагом главного спелла 
                _spells.Cust(_targetDetector); //_spells.StartCusting(_weapon);


                //_weapon.CauseDamage(_targetDetector);
                WeaponAttack();
            }
        }

        private void AttackIfTargetExists()
        {

            //if (target == null)
            //    return;

            //_specialization.ApplySpells(); - smth like that
        }

        private void WeaponAttack()
        {
            //_weapon.CauseDamage();
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
            //_targetDetector.Changed -= OnTargetChanged;
            //_targetDetector.OnNoneTarget -= StopAttack;
            _mainSpell.Custed -= () => Attacked?.Invoke();
        }
    }
}