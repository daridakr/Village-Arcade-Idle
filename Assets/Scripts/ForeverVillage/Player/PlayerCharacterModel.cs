using ForeverVillage.Scripts.Character;
using UnityEngine;

namespace ForeverVillage.Scripts
{
    public sealed class PlayerCharacterModel : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private PlayerTimerInteractions _interactions;

        private CharacterLoader _loader;
        private CustomizableCharacter _customizable;

        public CustomizableCharacter Customizable => _customizable;

        private void OnEnable() => _loader = new CharacterLoader();

        public void Setup(string path)
        {
            var prefab = _loader.LoadCustomizable(path);
            _customizable = Instantiate(prefab, transform);
            PlayerAnimation animation = _customizable.GetComponentInChildren<PlayerAnimation>();

            _movement.Setup(transform, animation);

            foreach(PlayerTimerInteractor interactor in _interactions.Interactors)
                interactor.Setup(animation, _customizable.HandRig);
        }
    }
}