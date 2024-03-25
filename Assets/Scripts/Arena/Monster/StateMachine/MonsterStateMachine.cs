using UnityEngine;

namespace Arena
{
    public class MonsterStateMachine : MonoBehaviour
    {
        [SerializeField] private TargetDetector _targetsDetector;
        [SerializeField] private State _first;

        private State _current;

        public State Current => _current;

        private void Start()
        {
            _current = _first;

            if (_current != null)
                _current.Enter(_targetsDetector);
        }

        private void Update()
        {
            if (_current == null)
                return;

            State next = _current.GetNext();

            if (next != null)
                Transit(next);
        }

        private void Transit(State nextState)
        {
            if (_current != null)
                _current.Exit();

            _current = nextState;

            if (_current != null)
                _current.Enter(_targetsDetector);
        }
    }
}