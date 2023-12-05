using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingListItemView : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private Image _image;
    [SerializeField] private ButtonTextDisplay _buyButton;

    private BuildingData _buildingData;

    public event Action<BuildingData> OnBuy;

    private void OnEnable()
    {
        _buyButton.Clicked += OnBuyButtonClicked;
    }

    public void Render(BuildingData buildingData, int playerBalance)
    {
        _title.text = buildingData.Name;
        _image.sprite = buildingData.Icon;
        _buyButton.ChangeTitle(buildingData.Price.ToString());
        _buyButton.SetInteractable(playerBalance >= buildingData.Price);

        _buildingData = buildingData;
    }

    private void OnBuyButtonClicked()
    {
        OnBuy?.Invoke(_buildingData);
    }

    private void OnDisable()
    {
        _buyButton.Clicked -= OnBuyButtonClicked;
    }
}
