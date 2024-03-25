using UnityEngine;

namespace Arena
{
    public sealed class ToChaseTransition : StateTransition
    {
        [SerializeField, Min(0f)] private float _stoppingDistance = 1.5f;

        private void Update()
        {
            if (Vector3.Distance(transform.position, NearestTarget.position) > _stoppingDistance)
                NeedTransit = true;
        }
    }
}