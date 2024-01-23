using UnityEngine;
using Zenject;

namespace ForeverVillage.Scripts.Character
{
    public class CharacterConfigurationSaver : MonoBehaviour
    {
        [SerializeField] private ConfirmCreationPopup _confirmCreationPopup;

        private ISpecializationRepository _specializationRepository;
        private ISpecializationsController _specializationController;

        private SpecializationSaver _specializationSaver;

        [Inject]
        public void Construct(ISpecializationRepository specializationRepository, ISpecializationsController specializationsController)
        {
            _specializationRepository = specializationRepository;
            _specializationController = specializationsController;
        }

        private void OnEnable()
        {
            _specializationSaver = new SpecializationSaver(_specializationRepository, _specializationController);
            _confirmCreationPopup.PlayButtonClicked += _specializationSaver.Save;
        }

        private void OnDisable()
        {
            _confirmCreationPopup.PlayButtonClicked -= _specializationSaver.Save;
        }
    }
}