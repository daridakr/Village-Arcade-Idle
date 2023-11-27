using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectList<T> : MonoBehaviour where T : ScriptableObject
{
    private List<T> _data = new List<T>();

    public IEnumerable<T> Data => _data;

    public void UnlockData(T reference, string guid)
    {
        _data.Add(reference);
        AfterUnlocked(reference as T, guid);
    }

    //private void OnUnlocked(T reference, string guid)
    //{
    //    _data.Add(reference);
    //    //Unlocked?.Invoke(reference as T, onLoad, guid);

    //    AfterUnlocked(reference as T, guid);
    //}

    protected virtual void AfterUnlocked(T reference, string guid) { }
}
