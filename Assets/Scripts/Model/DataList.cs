using System.Collections.Generic;
using UnityEngine;

public abstract class DataList<T> : MonoBehaviour
{
    private List<T> _data = new List<T>();

    public IEnumerable<T> Data => _data;

    public void Append(T reference, string guid)
    {
        _data.Add(reference);
        AfterAppended(reference, guid);
    }

    public void Append(T reference)
    {
        _data.Add(reference);
        AfterAppended(reference);
    }

    protected virtual void AfterAppended(T reference, string guid) { }
    protected virtual void AfterAppended(T reference) { }
}