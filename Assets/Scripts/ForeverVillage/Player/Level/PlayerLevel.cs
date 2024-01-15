using UnityEngine;
using UnityEngine.Events;

namespace ForeverVillage.Scripts
{
    public class PlayerLevel : MonoBehaviour,
        IGameSaveDataListener
    {
        private ExperienceLevel _level;

        public float Experience => _level.ExperienceNormalazed;
        public int Level => _level.Value;

        public event UnityAction<int> LevelChanged;
        public event UnityAction<float> ExperienceChanged;

        private void OnEnable()
        {
            _level = new ExperienceLevel(SaveKeyParams.Player.ExperienceLevel);
            _level.Load();

            _level.Increased += OnLevelIncreased;
            _level.ExperienceGained += OnExperienceGainded;
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

        public void OnSaveData(GameSaveReason reason)
        {
            _level.Save();
        }
    }
}