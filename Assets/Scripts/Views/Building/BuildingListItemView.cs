using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingListItemView : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private Image _image;
    [SerializeField] private ButtonTextView _buyButton;

    private Building _building;

    public Building BuildingData => _building;

    public event Action<BuildingListItemView> OnBuy;

    private void OnEnable()
    {
        _buyButton.Clicked += OnBuyButtonClicked;
    }

    public void Render(Building building, int playerBalance)
    {
        _title.text = building.Name;
        _buyButton.ChangeTitle(building.Price.ToString());
        _buyButton.SetInteractable(playerBalance >= building.Price);

        _building = building;
    }

    private void OnBuyButtonClicked()
    {
        OnBuy?.Invoke(this);
    }

    private void OnDisable()
    {
        _buyButton.Clicked -= OnBuyButtonClicked;
    }
}
