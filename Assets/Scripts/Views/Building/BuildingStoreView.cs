using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingStoreView : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private Image _image;
    [SerializeField] private ButtonTextDisplay _buyButton;

    private Building _building;

    public event Action<Building> OnBuy;

    private void OnEnable()
    {
        _buyButton.Clicked += OnBuyButtonClicked;
    }

    public void Render(Building building, int playerBalance)
    {
        _title.text = building.Data.Name;
        _image.sprite = building.Data.Icon;
        _buyButton.ChangeTitle(building.Data.Price.ToString());
        _buyButton.SetInteractable(playerBalance >= building.Data.Price);

        _building = building;
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
