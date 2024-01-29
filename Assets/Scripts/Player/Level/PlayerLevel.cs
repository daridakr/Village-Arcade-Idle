using UnityEngine;
using UnityEngine.Events;
using Village;

public abstract class PlayerLevel : MonoBehaviour
{
    protected ExperienceLevel _level;

    public float Experience => _level.ExperienceNormalazed;
    public int Level => _level.Value;

    public event UnityAction<int> LevelChanged;
    public event UnityAction<float> ExperienceChanged;

    protected void OnEnable()
    {
        Initialize();

        _level.Increased += OnLevelIncreased;
        _level.ExperienceGained += OnExperienceGainded;
    }

    private void OnLevelIncreased()
    {
        LevelChanged?.Invoke(Level);
        OnValueChanged();
    }

    private void OnExperienceGainded()
    {
        ExperienceChanged?.Invoke(Experience);
        OnValueChanged();
    }

    public void TakeExp(int value)
    {
        _level.AddExperience(value);
    }

    protected void OnDisable()
    {
        _level.Increased -= OnLevelIncreased;
        _level.ExperienceGained -= OnExperienceGainded;

        OnDisabled();
    }

    protected virtual void OnValueChanged() { }
    protected virtual void OnDisabled() { }
    protected abstract void Initialize();
}
