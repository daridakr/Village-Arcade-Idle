using UnityEngine;

public abstract class IntSavedValue : SavedValue<int>
{
    public override void Save(int value)
    {
        base.Save(value);
        PlayerPrefs.SetInt(Key, value);
    }

    public override int Get()
    {
        base.Get();
        return PlayerPrefs.GetInt(Key);
    }
}