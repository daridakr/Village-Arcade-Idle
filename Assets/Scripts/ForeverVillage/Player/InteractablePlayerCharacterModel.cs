using UnityEngine;

namespace Village
{
    public sealed class InteractablePlayerCharacterModel : PlayerCharacterModel
    {
        [SerializeField] private TimerInteractionsController _interactions;

        public override void Setup(string path)
        {
            base.Setup(path);

            foreach (TimerInteraction interactor in _interactions.Interactors)
                interactor.Setup(_animation, _instance.HandRig);
        }
    }
}