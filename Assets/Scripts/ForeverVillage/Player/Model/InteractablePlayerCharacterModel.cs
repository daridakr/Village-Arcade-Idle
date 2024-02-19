using System.Collections.Generic;
using UnityEngine;

namespace Village
{
    public sealed class InteractablePlayerCharacterModel : PlayerCharacterModel
    {
        [SerializeField] private TimerInteractionsController _interactionsController;

        private IEnumerable<IAnimatedInteraction> _animatedIteractions;

        protected override void OnSetuped()
        {
            _animatedIteractions = _interactionsController.GetAnimatedInteractions(_instance.HandRig);
        }

        public IEnumerable<IAnimatedInteraction> GetInteractions()
        {
            return _animatedIteractions;
        }
    }
}