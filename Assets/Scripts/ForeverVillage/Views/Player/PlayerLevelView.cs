using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ForeverVillage.Scripts
{
    public class PlayerLevelView : MonoBehaviour
    {
        [SerializeField] private PlayerLevel _playerLevel;
        [SerializeField] private Slider _levelProgressSlider;
        [SerializeField] private TMP_Text _levelTextDisplay;

        private void OnEnable()
        {
            _playerLevel.ExperienceChanged += OnExperienceChanged;
            _playerLevel.LevelChanged += OnLevelChanged;
        }

        private void OnExperienceChanged(float normalazedValue)
        {
            _levelProgressSlider.value = normalazedValue;
        }

        private void OnLevelChanged(int level)
        {
            _levelTextDisplay.text = level.ToString();
        }

        private void OnDisable()
        {
            _playerLevel.ExperienceChanged -= OnExperienceChanged;
            _playerLevel.LevelChanged -= OnLevelChanged;
        }
    }
}