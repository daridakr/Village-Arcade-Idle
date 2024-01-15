public abstract class DataRepository<T>
{
    protected abstract string _key { get; }

    protected bool LoadData(out T data)
    {
        if (Database.KeyExists(_key))
        {
            data = Database.Load<T>(_key);
            return true;
        }

        data = default;
        return false;
    }

    protected virtual void SaveData(T data)
    {
        Database.Save(_key, data);
    }
}
