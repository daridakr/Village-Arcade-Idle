using System;

namespace Village.Character
{
    public sealed class GenderPresenter
    {
        private readonly GenderConfig _config;
        private readonly GenderButtonView _view;

        private ISpecializationsController _specializationsController;

        public event Action<GenderButtonView> Clicked;

        public GenderPresenter(GenderConfig genderConfig, GenderButtonView buttonView)
        {
            _config = genderConfig;
            _view = buttonView;
        }

        public void Initialize(ISpecializationsController specializationsController)
        {
            _specializationsController = specializationsController;

            _view.SetIcon(_config.Icon);
            _view.Selected += OnGenderClicked;
        }

        private void OnGenderClicked(GenderButtonView selected)
        {
            _specializationsController.SetupSpecializationsFor(_config.Gender);
            Clicked?.Invoke(selected);
        }

        public void Dispose()
        {
            _view.Selected -= OnGenderClicked;
        }
    }
}