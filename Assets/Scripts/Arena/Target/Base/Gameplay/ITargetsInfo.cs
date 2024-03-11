namespace Arena
{
    public interface ITargetsInfo
    {
        public ITargetable[] All { get; }
        public Target Nearest { get; }
    }
}