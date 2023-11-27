using UnityEngine;

public abstract class SavedObject<T> where T : class
{
    private readonly string _saveKey;

    public SavedObject(string key)
    {
        _saveKey = key;
    }

    public void Save()
    {
        var jsonString = JsonUtility.ToJson(this as T);
        PlayerPrefs.SetString(_saveKey, jsonString);
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey(_saveKey) == false)
        {
            return;
        }

        var jsonString = PlayerPrefs.GetString(_saveKey);
        var loadedObject = JsonUtility.FromJson(jsonString, typeof(T));

        OnLoad(loadedObject as T);
    }

    protected abstract void OnLoad(T loadedObject);
}
