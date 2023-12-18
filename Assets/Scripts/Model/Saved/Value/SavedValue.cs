using UnityEngine;

public abstract class SavedValue<T> : MonoBehaviour, ISavedValue<T>
{
    public string Key { get; protected set; }

    public virtual T Get()
    {
        SetKey();
        return default(T);
    }

    public virtual void Save(T value)
    {
        SetKey();
    }

    protected abstract void SetKey();
}