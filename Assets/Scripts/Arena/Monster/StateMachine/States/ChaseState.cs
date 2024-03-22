using System;
using UnityEngine;

namespace Arena
{
    public class ChaseState : State
    {
        //public override event Action<float> OnEnter;
        //public override event Action OnExit;

        public event Action<Vector3> Updated;

        private void Update() => Updated?.Invoke(_target.position);
    }
}