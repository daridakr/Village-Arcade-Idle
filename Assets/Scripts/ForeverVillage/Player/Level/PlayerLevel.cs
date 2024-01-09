using UnityEngine;
using UnityEngine.Events;

namespace ForeverVillage.Scripts
{
    public class PlayerLevel : MonoBehaviour
    {
        private ExperienceLevel _level;

        private int Level => _level.Value;
        private float Experience => _level.ExperienceNormalazed;

        public event UnityAction<int> LevelChanged;
        public event UnityAction<float> ExperienceChanged;

        private void OnEnable()
        {
            _level = new ExperienceLevel(SaveKeyParams.Player.ExperienceLevel);
            _level.Load();

            _level.Increased += OnLevelIncreased;
            _level.ExperienceGained += OnExperienceGainded;

            LevelChanged?.Invoke(Level);
            ExperienceChanged?.Invoke(Experience);
        }

        private void OnLevelIncreased()
        {
            LevelChanged?.Invoke(Level);
            _level.Save();
        }

        private void OnExperienceGainded()
        {
            ExperienceChanged?.Invoke(Experience);
            _level.Save();
        }

        public void TakeExp(int value)
        {
            _level.AddExperience(value);
        }

        private void OnDisable()
        {
            _level.Increased -= OnLevelIncreased;
            _level.ExperienceGained -= OnExperienceGainded;

            _level.Save();
        }
    }
}