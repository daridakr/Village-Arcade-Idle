using UnityEngine;

namespace Arena
{
    public sealed class ToExitWinningTransition : StateTransition
    {
        [SerializeField] private float _winningTime = 1f;

        private void Update()
        {
            _winningTime -= Time.deltaTime;

            if (_winningTime <= 0f)
                NeedTransit = true;
        }
    }
}