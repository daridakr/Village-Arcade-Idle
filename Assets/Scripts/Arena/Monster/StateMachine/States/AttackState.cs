using System;

namespace Arena
{
    public class AttackState : State
    {
        public event Action Attacked;

        private void OnEnable() => Attack();

        private void Attack()
        {
            Attacked?.Invoke();
        }
    }
}