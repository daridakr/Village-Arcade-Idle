using System;
using System.Collections.Generic;
using UnityEngine;

namespace Arena
{
    public abstract class State : MonoBehaviour
    {
        [SerializeField] private List<StateTransition> _transitions;

        protected Transform _target;

        public virtual event Action OnEnter;
        public virtual event Action OnExit;

        public void Enter(Transform target)
        {
            if (enabled == false)
            {
                _target = target;
                enabled = true;

                foreach (var transition in _transitions)
                {
                    transition.enabled = true;
                    transition.Init(_target);
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