using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ForeverVillage.Scripts
{
    public class PlayerVillageInfo : StringSavedValue
    {
        [SerializeField] private StartGame _start;
        [SerializeField] private PlayerRegionsList _playerRegionsList;
        [SerializeField] private List<RegionPriceLock> _availableRegions;

        private string _name;
        private readonly PlayerLevel _playerLevel;

        //public int Level => _playerLevel.Level;

        public event Action<string> Named;

        private void Awake()
        {
            if (_start.IsNewGame)
            {
                _start.VillageNamed += OnVillageNamed;
            }
            else
            {
                _name = Get();
                Named?.Invoke(_name);
                PrepareRegions();
            }
        }

        private void OnVillageNamed(string name)
        {
            _start.VillageNamed -= OnVillageNamed;

            _name = name;
            Save(_name);

            Named?.Invoke(name);
            PrepareRegions();
        }

        private void PrepareRegions()
        {
            foreach (var availableRegion in _availableRegions)
            {
                availableRegion.Buyed += OnNewRegionBuyed;
            }
        }

        private void OnNewRegionBuyed(RegionPriceLock regionLock, RegionData regionData)
        {
            regionLock.Buyed -= OnNewRegionBuyed;
            _availableRegions.Remove(regionLock);

            _playerRegionsList.Append(regionData, regionData.Guid);
        }

        //[Inject]
        //public void Construct(PlayerLevel playerLevel)
        //{
        //    _playerLevel = playerLevel;
        //}

        protected override void SetKey()
        {
            Key = SaveKeyParams.Player.VillageName;
        }
    }
}