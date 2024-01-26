using ForeverVillage.Scripts.Character;
using UnityEngine;

namespace ForeverVillage.Scripts
{
    public sealed class PlayerCharacterModel : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private PlayerTimerInteractions _interactions;

        private CharacterLoader _loader;
        private CustomizableCharacter _instance;

        public CustomizableCharacter Customizable => _instance;

        private void OnEnable() => _loader = new CharacterLoader();

        public void Setup(string path)
        {
            CustomizableCharacter prefab = _loader.LoadCustomizable(path);
            _instance = Instantiate(prefab, transform);
            PlayerAnimation animation = _instance.GetComponentInChildren<PlayerAnimation>();

            _movement.Setup(transform, animation);

            foreach(PlayerTimerInteractor interactor in _interactions.Interactors)
                interactor.Setup(animation, _instance.HandRig);
        }
    }
}