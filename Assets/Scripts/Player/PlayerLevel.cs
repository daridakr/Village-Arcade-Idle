using UnityEngine;
using UnityEngine.Events;

public class PlayerLevel : MonoBehaviour
{
    private ExperienceLevel _level;

    public int Level => _level.Value;
    public float Experience => _level.ExperienceNormalazed;

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
        LevelChanged?.Invoke(_level.Value);
        _level.Save();
    }

    private void OnExperienceGainded()
    {
        ExperienceChanged?.Invoke(_level.ExperienceNormalazed);
        _level.Save();
    }

    public void GainExp(int value)
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
