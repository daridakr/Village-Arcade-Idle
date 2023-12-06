public interface ISavedValue<T>
{
    public string Key { get; }

    public void Save(T value);
    public T Get();
}
