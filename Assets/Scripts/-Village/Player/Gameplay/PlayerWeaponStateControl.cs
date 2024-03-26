using ForeverVillage;
using System;
using Zenject;

namespace Village
{
    public class PlayerWeaponStateControl :
        IInitializable,
        IDisposable
    {
        private ITimerInteractionsController _interactionsController;
        private IPlayerWeaponControl _weaponControl;

        [Inject]
        private void Construct(
            ITimerInteractionsController controller,
            IPlayerWeaponControl weaponControl)
        {
            _interactionsController = controller;
            _weaponControl = weaponControl;
        }

        public void Initialize()
        {
            _interactionsController.OnInteract += _weaponControl.Hide;
            _interactionsController.OnFinished += _weaponControl.Take;
        }

        public void Dispose()
        {
            _interactionsController.OnInteract -= _weaponControl.Hide;
            _interactionsController.OnFinished -= _weaponControl.Take;
        }
    }
}