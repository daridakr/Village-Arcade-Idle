using UnityEngine;
using Zenject;

namespace ForeverVillage.Scripts
{
    public class PlayerTimerInteractions : MonoBehaviour
    {
        [SerializeField] private PlayerTimerInteractor[] _interactors;

        private IInputService _inputService;

        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void OnEnable()
        {
            foreach (PlayerTimerInteractor interactor in _interactors)
            {
                interactor.Started += OnStarted;
                interactor.Stopped += OnStopped;
            }
        }

        private void OnStarted()
        {
            _inputService.Disable();
        }

        private void OnStopped()
        {
            _inputService.Enable();
        }

        private void OnDisable()
        {
            foreach (PlayerTimerInteractor interactor in _interactors)
            {
                interactor.Started -= OnStarted;
                interactor.Stopped -= OnStopped;
            }
        }
    }
}