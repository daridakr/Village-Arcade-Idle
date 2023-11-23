using System;
using UnityEngine;

public class ClearZoneCanvas : CanvasView
{
    [SerializeField] private PriceButtonView _clearButton;
    [SerializeField] private PriceView _priceView;

    public event Action ClearButtonClicked;

    private void OnEnable()
    {
        _clearButton.Clicked += OnClearButtonClicked;
    }

    public void Display(int price, int balance)
    {
        base.Display();
        _priceView.Init(price);
        _clearButton.SetInteractable(balance >= price);
    }

    private void OnClearButtonClicked()
    {
        Hide();
        ClearButtonClicked?.Invoke();
    }

    private void OnDisable()
    {
        _clearButton.Clicked -= OnClearButtonClicked;
    }
}
