using UnityEngine;

public abstract class StringSavedValue : SavedValue<string>
{
    public override void Save(string value)
    {
        base.Save(value);
        PlayerPrefs.SetString(Key, value);
    }

    public override string Get()
    {
        base.Get();
        return PlayerPrefs.GetString(Key);
    }
}