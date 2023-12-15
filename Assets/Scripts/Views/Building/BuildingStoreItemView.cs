using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingStoreItemView : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private HorizontalLayoutGroup _statsGroup;
    [SerializeField] private BuildingStatView _statTemplate;
    [SerializeField] private ButtonTextDisplay _buyButton;

    private Building _building;

    public event Action<Building> OnBuy;

    private void OnEnable()
    {
        _buyButton.Clicked += OnBuyButtonClicked;
    }

    public void Render(Building building, int playerBalance)
    {
        RenderStats(building.GetStatesWithIcons());

        _title.text = building.SpecificData.Name;
        _description.text = building.TypeData.Description;
        _image.sprite = building.SpecificData.MainIcon;
        _buyButton.ChangeTitle(building.SpecificData.Price.ToString());
        _buyButton.SetInteractable(playerBalance >= building.SpecificData.Price);

        _building = building;
    }

    private void RenderStats(Dictionary<Sprite, int> statsAndIcons)
    {
        if (statsAndIcons == null || statsAndIcons.Count() == 0)
        {
            return;
        }

        foreach (var stat in statsAndIcons)
        {
            BuildingStatView statView = Instantiate(_statTemplate, _statsGroup.transform);
            statView.Render(stat.Key, stat.Value.ToString());
        }
    }

    private void OnBuyButtonClicked()
    {
        OnBuy?.Invoke(_building);
    }

    private void OnDisable()
    {
        _buyButton.Clicked -= OnBuyButtonClicked;
    }
}
