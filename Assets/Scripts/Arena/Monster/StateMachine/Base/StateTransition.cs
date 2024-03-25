using System;
using UnityEngine;

namespace Arena
{
    public abstract class StateTransition : MonoBehaviour
    {
        [SerializeField] private State _targetState;

        private ITargetsInfo _targetInfo;
        protected Transform NearestTarget => _targetInfo.Nearest.transform;
        protected Target[] Targets => _targetInfo.All as Target[];

        public State TargetState => _targetState;
        public bool NeedTransit { get; protected set; }

        public void Init(ITargetsInfo targetInfo) => _targetInfo = targetInfo;

        private void OnEnable() => NeedTransit = false;
    }
}