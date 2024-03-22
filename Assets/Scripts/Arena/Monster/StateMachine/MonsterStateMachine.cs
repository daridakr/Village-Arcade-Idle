using UnityEngine;
using Zenject;

namespace Arena
{
    public class MonsterStateMachine : MonoBehaviour
    {
        [SerializeField] private State _first;

        private State _current;
        private Transform _target;

        public State Current => _current;

        [Inject]
        private void Construct(PlayerMovement player) => _target = player.transform;

        private void Start()
        {
            _current = _first;

            if (_current != null)
                _current.Enter(_target);
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
                _current.Enter(_target);
        }
    }
}