using UnityEngine;
using Village;

namespace Arena
{
    public sealed class MonsterPlayerSensor : MonoBehaviour
    {
        [SerializeField] private PlayerMovementTrigger _playerTrigger;

        public delegate void PlayerEnterEvent(Transform playerTransform);
        public delegate void PlayerExitEvent();

        public event PlayerEnterEvent OnPlayerEnter;
        public event PlayerExitEvent OnPlayerExit;

        private void OnEnable()
        {
            _playerTrigger.Enter += (player) => OnPlayerEnter?.Invoke(player.transform);
            _playerTrigger.Exit += (player) => OnPlayerExit?.Invoke();
        }
    }
}