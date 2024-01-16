using System;
using UnityEngine;
using Zenject;

namespace ForeverVillage.Scripts
{
    public class StartGame : IntSavedValue
    {
        [SerializeField] private InputFieldData _villageNameField;

        [SerializeField] private Building[] _buildingsInStart;
        [SerializeField] private Villager[] _villagersInStart;
        [SerializeField] private RegionDisplayer[] _regionsInStart;

        private PlayerBuildingsList _buildingList;
        private PlayerVillagersList _villagersList;
        private PlayerRegionsList _regionList;

        private bool BeginGame = true;

        public bool IsNewGame => Get() == 0;

        public event Action NewGameStarted;
        public event Action GameBegined;
        public event Action<string> VillageNamed;

        [Inject]
        public void Construct(PlayerBuildingsList buildings, PlayerVillagersList villagers, PlayerRegionsList regions)
        {
            _buildingList = buildings;
            _villagersList = villagers;
            _regionList = regions;
        }

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
