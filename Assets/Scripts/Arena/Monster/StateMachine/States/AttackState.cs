using System;

namespace Arena
{
    public class AttackState : State
    {
        // future: for multiplayer when monsters can use aoe attack for many players
        public event Action<ITargetsInfo> Attacked;

        private void OnEnable() => Attacked?.Invoke(_targetInfo);

        private void Update()
        {
            //_spell.Custed += () => Attacked?.Invoke();
            //
        }
    }
}