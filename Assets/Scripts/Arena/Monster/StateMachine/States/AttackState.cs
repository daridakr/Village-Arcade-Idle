using System;

namespace Arena
{
    public class AttackState : State
    {
        public event Action<ITargetsInfo> Attacked;

        private void OnEnable() => Attacked?.Invoke(_targetInfo);
    }
}