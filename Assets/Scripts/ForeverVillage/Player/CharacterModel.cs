using ForeverVillage.Scripts.Character;
using UnityEngine;

namespace ForeverVillage.Scripts
{
    public class CharacterModel : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private PlayerTimerInteractions _interactions;

        private CharacterLoader _loader;

        private void OnEnable()
        {
            _loader = new CharacterLoader();
        }

        public void Setup(string path)
        {
            var prefab = _loader.LoadCustomizable(path);
            CustomizableCharacter model = Instantiate(prefab, transform);
            PlayerAnimation animation = model.GetComponentInChildren<PlayerAnimation>();

            _movement.Setup(transform, animation);

            foreach(PlayerTimerInteractor interactor in _interactions.Interactors)
                interactor.Setup(animation, model.HandRig);

            Destroy(model);
        }
    }
}