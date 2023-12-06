using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingListView : CanvasAnimatedView
{
    [SerializeField] private BuildingListItemView _listViewItem;
    [SerializeField] private VerticalLayoutGroup _content;

    [SerializeField] private PlayerBuildingsList _availableBuildings;
    [SerializeField] private PlayerMoney _moneyOwner;

    private List<BuildingListItemView> _buildingViews = new List<BuildingListItemView>();

    public event Action<BuildingData> OnSmthBuyed;

    public override void Display()
    {
        base.Display();
        ClearOldData();

        foreach (BuildingData building in _availableBuildings.Data)
        {
            BuildingListItemView buildingView = Instantiate(_listViewItem, _content.transform);
            buildingView.Render(building, _moneyOwner.Balance);
            buildingView.OnBuy += OnBuildingBuyed;
            _buildingViews.Add(buildingView);
        }
    }

    private void OnBuildingBuyed(BuildingData buildingData)
    {
        Hide();
        OnSmthBuyed?.Invoke(buildingData);
    }

    private void ClearOldData()
    {
        foreach(BuildingListItemView buildingView in _buildingViews)
        {
            buildingView.OnBuy -= OnBuildingBuyed;
            Destroy(buildingView.gameObject);
        }

        _buildingViews.Clear();
    }
}
