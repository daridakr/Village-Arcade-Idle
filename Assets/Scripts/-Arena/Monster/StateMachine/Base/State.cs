using System;
using System.Collections.Generic;
using UnityEngine;

namespace Arena
{
    public abstract class State : MonoBehaviour
    {
        [SerializeField] private List<StateTransition> _transitions;

        protected ITargetsInfo _targetInfo;
        protected Transform NearestTarget => _targetInfo.Nearest.transform;

        public event Action OnEnter;
        public event Action OnExit;

        public void Enter(ITargetsInfo targetInfo)
        {
            if (enabled == false)
            {
                _targetInfo = targetInfo;
                enabled = true;

                foreach (var transition in _transitions)
                {
                    transition.Init(_targetInfo);
                    transition.enabled = true;
                }

                OnEnter?.Invoke();
            }
        }

        public State GetNext()
        {
            foreach (var transition in _transitions)
            {
                if (transition.NeedTransit)
                    return transition.TargetState;
            }

            return null;
        }

        public void Exit()
        {
            if (enabled)
            {
                foreach (var transition in _transitions)
                    transition.enabled = false;

                enabled = false;

                OnExit?.Invoke();
            }
        }
    }
}