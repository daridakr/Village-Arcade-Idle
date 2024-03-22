using UnityEngine;

namespace Arena
{
    public class ToAttackTransition : StateTransition
    {
        [SerializeField, Min(0f)] private float _stoppingDistance = 1.5f;

        private void Update()
        {
            if (Vector3.Distance(transform.position, _target.position) < _stoppingDistance)
                NeedTransit = true;
        }
    }
}