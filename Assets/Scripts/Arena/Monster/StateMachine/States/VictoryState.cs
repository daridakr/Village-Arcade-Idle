using System;

namespace Arena
{
    public sealed class VictoryState : State
    {
        public event Action Winned;

        private void OnEnable() => Winned?.Invoke();
    }
}