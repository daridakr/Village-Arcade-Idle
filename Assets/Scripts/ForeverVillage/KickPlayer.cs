using UnityEngine;

namespace Village
{
    [RequireComponent(typeof(Collider))]
    public class KickPlayer : MonoBehaviour
    {
        [SerializeField] private PlayerMovementTrigger _playerTrigger;
        [SerializeField] private Transform _pointToKick;

        private void OnEnable()
        {
            _playerTrigger.Stay += OnPlayerTriggerStay;
            _playerTrigger.Enter += OnPlayerTriggerEnter;
        }

        private void OnPlayerTriggerStay(UpgradablePlayerMovement playerMovement)
        {
            playerMovement.MoveToTarget(_pointToKick);
            _playerTrigger.Stay -= OnPlayerTriggerStay;
            _playerTrigger.Enter -= OnPlayerTriggerEnter;
        }

        private void OnPlayerTriggerEnter(UpgradablePlayerMovement playerMovement)
        {
            _playerTrigger.Enter -= OnPlayerTriggerEnter;
            _playerTrigger.Stay -= OnPlayerTriggerStay;
        }
    }
}