using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Village
{
    public class PlayerVillageInfo : MonoBehaviour
    {
        [SerializeField] private List<RegionPriceLock> _availableRegions;

        private PlayerRegionsList _playerRegionsList;

        [Inject]
        private void Construct(PlayerRegionsList regions)
        {
            _playerRegionsList = regions;
        }

        private void Awake()
        {
            PrepareRegions();
        }

        private void PrepareRegions()
        {
            foreach (var availableRegion in _availableRegions)
            {
                availableRegion.Buyed += OnNewRegionBuyed;
            }
        }

        private void OnNewRegionBuyed(RegionPriceLock regionLock, RegionDisplayer regionData)
        {
            regionLock.Buyed -= OnNewRegionBuyed;
            _availableRegions.Remove(regionLock);

            _playerRegionsList.Append(regionData, regionData.Guid);
        }
    }
}