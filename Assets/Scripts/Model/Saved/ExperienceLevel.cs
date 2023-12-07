using System;
using UnityEngine;

[Serializable]
public class ExperienceLevel : KeySavedObject<ExperienceLevel>
{
    [SerializeField] private int _value;
    [SerializeField] private int _experience;
    private int _experienceToNext;

    public int Value => _value;
    public float ExperienceNormalazed => (float)_experience / _experienceToNext;

    public event Action Increased;
    public event Action ExperienceGained;

    public ExperienceLevel(string saveKey) : base(saveKey)
    {
        _value = 1;
        _experienceToNext = 100;
    }

    public void AddExperience(int amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount));
        }

        _experience += amount;
        CheckForNewLevel();
        ExperienceGained?.Invoke();
    }

    private void CheckForNewLevel()
    {
        while (_experience >= _experienceToNext)
        {
            _value++;
            _experience -= _experienceToNext;
            Increased?.Invoke();
        }
    }

    protected override void OnLoad(ExperienceLevel loadedObject)
    {
        _value = loadedObject._value;
        _experience = loadedObject._experience;
    }
}
