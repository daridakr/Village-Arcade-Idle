using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Village
{
    public class PlayerTimerInteractions : MonoBehaviour
    {
        [SerializeField] private PlayerTimerInteractor[] _interactors;

        private IInputService _inputService;

        public IEnumerable<PlayerTimerInteractor> Interactors => _interactors;

        [Inject]
        private void Construct(IInputService inputService)
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

        private void OnStarted(PlayerTimerInteractor interactor)
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