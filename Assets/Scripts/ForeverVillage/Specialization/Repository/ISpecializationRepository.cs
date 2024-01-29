namespace Village
{
    public interface ISpecializationRepository
    {
        public bool Load(out SpecializationData specialization);
        public void Save(SpecializationData specialization);
    }
}