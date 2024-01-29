using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

namespace Village
{
    public class PlayerLevelView : MonoBehaviour
    {
        [SerializeField] private Slider _levelProgressSlider;
        [SerializeField] private TMP_Text _levelTextDisplay;
        
        private SavedPlayerLevel _playerLevel;

        [Inject]
        private void Construct(SavedPlayerLevel playerLevel)
        {
            _playerLevel = playerLevel;
        }

        private void OnEnable()
        {
            _playerLevel.ExperienceChanged += OnExperienceChanged;
            _playerLevel.LevelChanged += OnLevelChanged;
        }

        private void Start()
        {
            _levelTextDisplay.text = _playerLevel.Level.ToString();
            _levelProgressSlider.value = _playerLevel.Experience;
        }

        private void OnLevelChanged(int level)
        {
            _levelTextDisplay.text = level.ToString();
        }

        private void OnExperienceChanged(float normalazedValue)
        {
            _levelProgressSlider.value = normalazedValue;
        }

        private void OnDisable()
        {
            _playerLevel.ExperienceChanged -= OnExperienceChanged;
            _playerLevel.LevelChanged -= OnLevelChanged;
        }
    }
}