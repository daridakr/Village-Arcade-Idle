namespace ForeverVillage.Scripts
{
    public class BuildingsStore : Store<Building>
    {
        //private BuildingsStore _store;

        private void OnEnable()
        {
            //_store = new BuildingsStore(SaveKeyParams.Game.BuildingStore);
            //_store.Load();

            //_store.ValueChanged += OnBalanceChanged;
        }

        public void Add(Building building)
        {
            //_store.Add(building);
        }

        //public IEnumerable<BuildingData> GetBuildingsData()
        //{
        //    List<BuildingData> _datas = new List<BuildingData>();

        //    foreach (Building building in _availableBuildings.Data)
        //    {
        //        _datas.Add(building.Data);
        //    }

        //    return _datas;
        //}
    }
}