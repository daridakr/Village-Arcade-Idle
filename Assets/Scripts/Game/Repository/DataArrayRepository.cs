public abstract class DataArrayRepository<T>
{
    protected abstract string _key { get; }

    protected bool LoadData(out T[] data)
    {
        if (Database.KeyExists(_key))
        {
            data = Database.Load<T[]>(_key);
            return true;
        }

        data = null;
        return false;
    }

    protected void SaveData(T[] data)
    {
        Database.Save(_key, data);
    }
}