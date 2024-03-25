using System;
using UnityEngine;

namespace Arena
{
    public class ChaseState : State
    {
        public event Action<Vector3> Updated;

        private void Update() => Updated?.Invoke(NearestTarget.position);
    }
}