namespace Arena
{
    public interface ISpellEntity
    {
        public float CastingTime { get; }
        public float LifeTime { get; }
        public float Cooldown { get; }
    }
}