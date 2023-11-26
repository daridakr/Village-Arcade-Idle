using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingListView : CanvasAnimatedView
{
    [SerializeField] private BuildingListItemView _prefab;
    [SerializeField] private VerticalLayoutGroup _content;
    // temp. in future - class smth like PlayerBuildings or PlayerBluePrints
    [SerializeField] private Building[] _availableBuldings;
    [SerializeField] private MoneyOwner _moneyOwner;

    private List<BuildingListItemView> _buildingViews = new List<BuildingListItemView>();

    public event Action<BuildingListItemView> OnBuyed;

    public override void Display()
    {
        base.Display();
        ClearOldData();

        for (int i = 0; i < _availableBuldings.Length; i++)
        {
            BuildingListItemView buildingView = Instantiate(_prefab, _content.transform);
            buildingView.Render(_availableBuldings[i], _moneyOwner.Balance);
            buildingView.OnBuy += OnBuildingBuyed;
            _buildingViews.Add(buildingView);
        }
    }

    private void OnBuildingBuyed(BuildingListItemView buildingViewItem)
    {
        Hide();
        OnBuyed?.Invoke(buildingViewItem);
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
