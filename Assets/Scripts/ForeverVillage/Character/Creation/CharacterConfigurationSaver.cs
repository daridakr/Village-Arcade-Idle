using IJunior.TypedScenes;
using UnityEngine;
using Zenject;

namespace ForeverVillage.Scripts.Character
{
    public class CharacterConfigurationSaver : MonoBehaviour
    {
        [SerializeField] private ConfirmCreationPopup _confirmCreationPopup;

        private ISpecializationRepository _specializationRepository;
        private ISpecializationsController _specializationController;
        private ICustomizationsRepository _customizationsRepository;
        private ICustomizationsController _customizationsController;

        private SpecializationSaver _specializationSaver;
        private CustomizationsSaver _customizationsSaver;

        [Inject]
        public void Construct(
            ISpecializationRepository specializationRepository,
            ISpecializationsController specializationsController,
            ICustomizationsRepository customizationsRepository,
            ICustomizationsController customizationsController)
        {
            _specializationRepository = specializationRepository;
            _specializationController = specializationsController;
            _customizationsRepository = customizationsRepository;
            _customizationsController = customizationsController;
        }

        private void OnEnable()
        {
            _specializationSaver = new SpecializationSaver(_specializationRepository, _specializationController);
            _customizationsSaver = new CustomizationsSaver(_customizationsRepository, _customizationsController);

            _confirmCreationPopup.PlayButtonClicked += SaveAndStartGame;
        }

        private void SaveAndStartGame()
        {
            _specializationSaver.Save();
            _customizationsSaver.Save();
            Main.Load();
        }

        private void OnDisable()
        {
            _confirmCreationPopup.PlayButtonClicked -= SaveAndStartGame;
        }
    }
}