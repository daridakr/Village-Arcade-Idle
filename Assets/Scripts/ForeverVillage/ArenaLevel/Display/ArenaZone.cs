using UnityEngine;

namespace Village
{
    public class ArenaZone : MonoBehaviour
    {
        [SerializeField] private PlayerTrigger _playerTrigger;
        [SerializeField] private CanvasAnimatedView _view;

        private void OnEnable()
        {
            _playerTrigger.Enter += OnPlayerTriggerEnter;
            _playerTrigger.Exit += OnPlayerTriggerExit;
        }

        private void OnPlayerTriggerEnter(PlayerReference player)
        {
            _view.Display();
        }

        private void OnPlayerTriggerExit(PlayerReference wallet)
        {
            _view.Hide();
        }

        private void OnDisable()
        {
            _playerTrigger.Enter -= OnPlayerTriggerEnter;
            _playerTrigger.Exit -= OnPlayerTriggerExit;
        }
    }
}