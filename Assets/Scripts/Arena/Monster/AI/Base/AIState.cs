using System;
using UnityEngine;

namespace Arena
{
    public abstract class AIState : MonoBehaviour
    {
        public virtual float GetWeight() => 100f;
        public virtual void OnEnterState(AIState previousState) { }
        public virtual void WhileInState() { }
        public virtual void OnExitState(AIState nextState) { }
        public abstract bool CanEnterState();
        public abstract bool CanExitState();
        public abstract event Action<AIState[]> CanTransit;
        public virtual event Action<Vector3> Updated;
    }
}