using UnityEngine;

public abstract class KeySavedObject<T> where T : class
{
    private readonly string _key;

    public KeySavedObject(string key)
    {
        _key = key;
    }

    public void Save()
    {
        var jsonString = JsonUtility.ToJson(this as T);
        PlayerPrefs.SetString(_key, jsonString);
    }

    public void Delete()
    {
        PlayerPrefs.DeleteKey(_key);
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey(_key) == false)
        {
            Save();
        }

        var jsonString = PlayerPrefs.GetString(_key);
        var loadedObject = JsonUtility.FromJson(jsonString, typeof(T));

        OnLoad(loadedObject as T);
    }

    protected abstract void OnLoad(T loadedObject);
}
