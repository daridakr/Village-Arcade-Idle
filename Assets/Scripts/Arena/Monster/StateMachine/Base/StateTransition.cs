using UnityEngine;

namespace Arena
{
    public abstract class StateTransition : MonoBehaviour
    {
        [SerializeField] private State _targetState;

        protected Transform _target;

        public State TargetState => _targetState;
        public bool NeedTransit { get; protected set; }

        public void Init(Transform target) => _target = target;

        private void OnEnable() => NeedTransit = false;
    }
}