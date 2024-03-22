using System;

namespace Arena
{
    public class VictoryState : State
    {
        public event Action Winned;

        private void OnEnable() => Winned?.Invoke();
    }
}