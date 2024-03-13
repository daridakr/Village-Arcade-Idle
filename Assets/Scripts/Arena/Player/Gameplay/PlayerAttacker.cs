using ForeverVillage;
using UnityEngine;

namespace Arena
{
    public sealed class PlayerAttacker : MonoBehaviour
    {
        [SerializeField] private TargetDetector _targetDetector;
        [SerializeField] private PlayerSpells _spells;
        [SerializeField] private PlayerWeapon _weapon;

        private float _totalDamage;

        private void OnEnable()
        {
            //_targetDetector.Changed += OnTargetChanged;
            _targetDetector.OnNoneTarget += StopAttack;
        }

        private void Update()
        {
            if (_targetDetector.IsTargetDetected)
            {
                // в общий дамаг плюсуется дамаг от оружия с дамагом главного спелла 
                _spells.Activate(_targetDetector);
                //_weapon.CauseDamage(_targetDetector);
                Attack();
            }
        }

        private void AttackIfTargetExists()
        {

            //if (target == null)
            //    return;

            //_specialization.ApplySpells(); - smth like that
        }

        private void Attack()
        {
            //_weapon.CauseDamage();
        }

        private void StopAttack()
        {

        }

        private void OnDisable()
        {
            //_targetDetector.Changed -= OnTargetChanged;
            _targetDetector.OnNoneTarget -= StopAttack;
        }
    }
}