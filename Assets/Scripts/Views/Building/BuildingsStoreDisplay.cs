using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingsStoreDisplay : CanvasAnimatedView
{
    [SerializeField] private BuildingStoreView _buildingStoreTemplate;
    [SerializeField] private VerticalLayoutGroup _content;

    [SerializeField] private BuildingStore _store;
    [SerializeField] private PlayerMoney _moneyOwner;

    private List<BuildingStoreView> _buildingStoreViews = new List<BuildingStoreView>();

    public event Action<Building> OnSmthBuyed;

    public override void Display()
    {
        base.Display();
        ClearOldData();

        foreach (Building building in _store.Buildings)
        {
            BuildingStoreView buildingStoreView = Instantiate(_buildingStoreTemplate, _content.transform);
            buildingStoreView.Render(building, _moneyOwner.Balance);
            buildingStoreView.OnBuy += OnBuildingBuyed;
            _buildingStoreViews.Add(buildingStoreView);
        }
    }

    private void OnBuildingBuyed(Building building)
    {
        Hide();
        OnSmthBuyed?.Invoke(building);
    }

    private void ClearOldData()
    {
        foreach(BuildingStoreView buildingView in _buildingStoreViews)
        {
            buildingView.OnBuy -= OnBuildingBuyed;
            Destroy(buildingView.gameObject);
        }

        _buildingStoreViews.Clear();
    }
}
