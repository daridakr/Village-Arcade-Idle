using System;
using System.Linq;
using UnityEngine;

namespace Arena
{
    public sealed class AIStateMachine : MonoBehaviour
    {
        [SerializeField, Min(0.1f)] private float _stateCheckingTick = 0.5f;
        [SerializeField] private AIState[] _states;

        private AIState _currentState;
        public event Action<AIState, AIState> OnStateChanged;

        private void OnEnable()
        {
            foreach (var state in _states)
                state.CanTransit += TransitState;
        }

        private void Start()
        {
            OnStateMachineTick();
            InvokeRepeating(nameof(OnStateMachineTick), UnityEngine.Random.Range(0f, _stateCheckingTick), _stateCheckingTick);
        }

        private void OnStateMachineTick()
        {
            if (_currentState && !_currentState.CanExitState())
                return;

            TransitionTo(GetValidNextState());
        }

        private void TransitState(params AIState[] ignoredStates)
        {
            TransitionTo(GetValidNextState(ignoredStates));
        }

        public AIState GetValidNextState(params AIState[] ignoredStates)
        {
            (AIState state, float weight)[] validStatesWithWeights = _states.Except(ignoredStates).
                Where(state => state.CanEnterState()).
                Select(state => (state, weight: state.GetWeight())).
                Where(State_Weight_Pair => State_Weight_Pair.weight > 0f).ToArray();

            float totalWeight = validStatesWithWeights.Sum(State_Weight_Pair => State_Weight_Pair.weight);
            float randomWeight = UnityEngine.Random.Range(0f, totalWeight);

            foreach (var (state, weight) in validStatesWithWeights)
            {
                randomWeight -= weight;
                if (randomWeight <= 0f)
                {
                    return state;
                }
            }

            return null;
        }

        public void TransitionTo(AIState nextState)
        {
            AIState previousState = _currentState;
            _currentState = nextState;

            if (previousState)
            {
                previousState.OnExitState(nextState);
            }

            if (_currentState)
            {
                _currentState.OnEnterState(_currentState);
            }

            OnStateChanged?.Invoke(previousState, _currentState);
        }

        private void Update()
        {
            if (_currentState)
            {
                _currentState.WhileInState();
            }
        }

        private void OnDisable()
        {
            foreach (var state in _states)
                state.CanTransit -= TransitState;
        }
    }
}