using System;
using UnityEngine;

namespace Arena
{
    public class AttackState : State
    {
        [SerializeField] private DamageSpell _spell;

        public event Action Attacked;

        private void Update()
        {
            //_spell.Custed += () => Attacked?.Invoke();
            //_spell.StartCusting(this, _target);
        }
    }
}