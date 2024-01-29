namespace Village
{
    public sealed class SpecializationRepository : DataRepository<SpecializationData>,
        ISpecializationRepository
    {
        protected override string _key => "PlayerSpecialization";

        public bool Load(out SpecializationData specialization)
        {
            return LoadData(out specialization);
        }

        public void Save(SpecializationData specialization)
        {
            SaveData(specialization);
        }
    }
}