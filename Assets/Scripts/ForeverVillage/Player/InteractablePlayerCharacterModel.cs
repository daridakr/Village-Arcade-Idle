using UnityEngine;

namespace Village
{
    public sealed class InteractablePlayerCharacterModel : PlayerCharacterModel
    {
        [SerializeField] private PlayerTimerInteractions _interactions;

        public override void Setup(string path)
        {
            base.Setup(path);

            foreach (PlayerTimerInteractor interactor in _interactions.Interactors)
                interactor.Setup(_animation, _instance.HandRig);
        }
    }
}