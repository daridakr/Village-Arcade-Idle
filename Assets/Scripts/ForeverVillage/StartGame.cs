using System;
using UnityEngine;

namespace ForeverVillage.Scripts
{
    public class StartGame : IntSavedValue
    {
        [SerializeField] private InputFieldData _villageNameField;

        [SerializeField] private PlayerBuildingsList _buildingList;
        [SerializeField] private Building[] _buildingsInStart;

        [SerializeField] private PlayerVillagersList _villagersList;
        [SerializeField] private Villager[] _villagersInStart;

        [SerializeField] private PlayerRegionsList _regionList;
        [SerializeField] private RegionDisplayer[] _regionsInStart;

        private bool BeginGame = true;

        public bool IsNewGame => Get() == 0;

        public event Action NewGameStarted;
        public event Action GameBegined;
        public event Action<string> VillageNamed;

        private void Start()
        {
            if (IsNewGame)
            {
                NewGameStarted?.Invoke();
                _villageNameField.DataGetted += OnVilageNamed;
            }
        }

        private void OnVilageNamed(string name)
        {
            VillageNamed?.Invoke(name);
            GameBegined?.Invoke();

            AddStartBuildings();
            AddStartVillagers();
            AddStartRegions();
            Save(Convert.ToInt32(BeginGame));
        }

        private void AddStartBuildings()
        {
            foreach (var building in _buildingsInStart)
            {
                _buildingList.Append(building);
            }
        }

        private void AddStartVillagers()
        {
            foreach (var villager in _villagersInStart)
            {
                _villagersList.Append(villager);
            }
        }

        private void AddStartRegions()
        {
            foreach (var region in _regionsInStart)
            {
                _regionList.Append(region, region.Guid);
            }
        }

        protected override void SetKey()
        {
            Key = SaveKeyParams.Game.New;
        }
    }
}
