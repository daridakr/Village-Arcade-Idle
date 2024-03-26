namespace Village
{
    public sealed class PlayerPointRepository : DataRepository<PlayerPointData>
    {
        protected override string _key => "PlayerPointData";

        public bool Load(out PlayerPointData playerPoint)
        {
            return LoadData(out playerPoint);
        }

        public void Save(PlayerPointData playerPoint)
        {
            SaveData(playerPoint);
        }
    }
}