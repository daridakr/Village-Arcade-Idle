using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Village
{
    public class TimerInteractionsController : MonoBehaviour,
        ITimerInteractionsController
    {
        [SerializeField] private AnimatedTimerInteraction[] _interactors;

        private List<IAnimatedInteraction> _animatedInteractions;
        private IInputService _inputService;

        public event Action OnInteract;
        public event Action OnFinished;

        [Inject]
        private void Construct(IInputService inputService) => _inputService = inputService;

        public IEnumerable<IAnimatedInteraction> GetAnimatedInteractions(Transform rigForItem = null)
        {
            foreach (AnimatedTimerInteraction interaction in _interactors)
            {
                interaction.Setup(rigForItem);
                _animatedInteractions.Add(interaction);
            }

            return _animatedInteractions;
        }

        private void OnEnable()
        {
            foreach (TimerInteraction interactor in _interactors)
            {
                interactor.Started += OnStarted;
                interactor.Stopped += OnStopped;
            }

            _animatedInteractions = new List<IAnimatedInteraction>();
        }

        private void OnStarted()
        {
            _inputService.Disable();

            OnInteract?.Invoke();
        }

        private void OnStopped()
        {
            _inputService.Enable();

            OnFinished?.Invoke();
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