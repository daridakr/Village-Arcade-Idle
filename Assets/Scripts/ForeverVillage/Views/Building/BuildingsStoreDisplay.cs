using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ForeverVillage.Scripts
{
    public class BuildingsStoreDisplay : CanvasAnimatedView
    {
        [SerializeField] private BuildingStoreItemView _buildingStoreTemplate;
        [SerializeField] private VerticalLayoutGroup _content;

        [SerializeField] private BuildingStore _store;
        [SerializeField] private PlayerMoney _moneyOwner;

        [SerializeField] private Button _mainTab;
        [SerializeField] private Image _tabFocus;

        private List<BuildingStoreItemView> _buildingStoreViews = new List<BuildingStoreItemView>();
        private Image _currentTabFocus;

        public event Action<Building> OnSmthBuyed;

        public override void Display()
        {
            base.Display();
            ClearOldData();

            _mainTab.Select();
            _currentTabFocus = Instantiate(_tabFocus, _mainTab.transform);

            foreach (Building building in _store.Buildings)
            {
                // for tabs allocation
                if (building.TryGetComponent(out ResidentialBuilding residential))
                {

                }

                BuildingStoreItemView buildingStoreView = Instantiate(_buildingStoreTemplate, _content.transform);
                buildingStoreView.Render(building, _moneyOwner.Balance);
                buildingStoreView.OnBuy += OnBuildingBuyed;
                _buildingStoreViews.Add(buildingStoreView);
            }
        }

        public void OnTabSelected(Transform tab)
        {
            Destroy(_currentTabFocus.gameObject);
            _currentTabFocus = Instantiate(_tabFocus, tab);
        }

        private void OnBuildingBuyed(Building building)
        {
            Hide();
            OnSmthBuyed?.Invoke(building);
        }

        private void ClearOldData()
        {
            foreach (BuildingStoreItemView buildingView in _buildingStoreViews)
            {
                buildingView.OnBuy -= OnBuildingBuyed;
                Destroy(buildingView.gameObject);
            }

            _buildingStoreViews.Clear();
        }
    }
}