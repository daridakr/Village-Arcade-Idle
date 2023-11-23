using UnityEngine;

public abstract class SavedObject<T> where T : class
{
    private readonly string _key;

    public SavedObject(string key)
    {
        _key = key;
    }

    public void Save()
    {
        var jsonString = JsonUtility.ToJson(this as T);
        PlayerPrefs.SetString(_key, jsonString);
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey(_key) == false)
        {
            return;
        }

        var jsonString = PlayerPrefs.GetString(_key);
        var loadedObject = JsonUtility.FromJson(jsonString, typeof(T));

        OnLoad(loadedObject as T);
    }

    protected abstract void OnLoad(T loadedObject);
}
