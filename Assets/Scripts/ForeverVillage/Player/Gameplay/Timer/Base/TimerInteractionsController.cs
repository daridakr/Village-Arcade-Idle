using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Village
{
    public class TimerInteractionsController : MonoBehaviour
    {
        [SerializeField] private TimerInteraction[] _interactors;

        private IInputService _inputService;

        public IEnumerable<TimerInteraction> Interactors => _interactors;

        [Inject]
        private void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void OnEnable()
        {
            foreach (TimerInteraction interactor in _interactors)
            {
                interactor.Started += OnStarted;
                interactor.Stopped += OnStopped;
            }
        }

        private void OnStarted(TimerInteraction interactor)
        {
            _inputService.Disable();
        }

        private void OnStopped()
        {
            _inputService.Enable();
        }

        private void OnDisable()
        {
            foreach (TimerInteraction interactor in _interactors)
            {
                interactor.Started -= OnStarted;
                interactor.Stopped -= OnStopped;
            }
        }
    }
}