using System.Collections.Generic;

namespace Village
{
    public sealed class InteractablePlayerAnimation : PlayerAnimation
    {
        private IEnumerable<IAnimatedInteraction> _animatedInteractions;

        private IAnimatedInteraction _activeAnimated;
        private InteractablePlayerCharacterModel _interactableModel;

        private void OnEnable()
        {
            _interactableModel = _model as InteractablePlayerCharacterModel;
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

        protected override void OnModelInitialized()
        {
            base.OnModelInitialized();

            InitAnimatedInteractions();
        }

        private void InitAnimatedInteractions()
        {
            _animatedInteractions = _interactableModel.GetInteractions();

            foreach (IAnimatedInteraction interaction in _animatedInteractions)
            {
                interaction.Started += SetAnimation;
                interaction.Stopped += StopAnimation;
            }
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