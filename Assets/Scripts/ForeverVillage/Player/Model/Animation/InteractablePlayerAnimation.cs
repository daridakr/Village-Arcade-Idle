using System.Collections.Generic;
using UnityEngine;

namespace Village
{
    public sealed class InteractablePlayerAnimation : PlayerAnimation
    {
        [SerializeField] private TimerInteractionsController _interactionsController;

        private IEnumerable<IAnimatedInteraction> _animatedInteractions;
        private IAnimatedInteraction _activeAnimated;

        protected override void OnModelInitialized()
        {
            base.OnModelInitialized();

            InitAnimatedInteractions();
        }

        private void InitAnimatedInteractions()
        {
            _animatedInteractions = _interactionsController.GetAnimatedInteractions(_model.HandRigR);

            foreach (IAnimatedInteraction interaction in _animatedInteractions)
            {
                interaction.Started += SetAnimation;
                interaction.Stopped += StopAnimation;
            }
        }

        private void SetAnimation(IAnimatedInteraction animated)
        {
            if (_activeAnimated != null)
                return;

            _activeAnimated = animated;
            AnimateActiveInteraction();
        }

        private void StopAnimation()
        {
            StopActiveInteraction();
            _activeAnimated = null;
        }

        private void AnimateActiveInteraction()
        {
            if (_animator && _activeAnimated != null)
                _animator.SetBool(_activeAnimated.AnimationParam, true);
        }

        private void StopActiveInteraction()
        {
            if (_animator && _activeAnimated != null)
                _animator.SetBool(_activeAnimated.AnimationParam, false);
        }


        private void OnDisable()
        {
            foreach (IAnimatedInteraction interaction in _animatedInteractions)
            {
                interaction.Started -= SetAnimation;
                interaction.Stopped -= StopAnimation;
            }
        }
    }
}