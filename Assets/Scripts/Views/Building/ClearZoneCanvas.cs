using System;
using UnityEngine;

public class ClearZoneCanvas : CanvasAnimatedView
{
    [SerializeField] private ButtonView _clearButton;
    [SerializeField] private TextDisplay _priceView;

    public event Action ClearButtonClicked;

    private void OnEnable()
    {
        _clearButton.Clicked += OnClearButtonClicked;
    }

    public void Display(int price, int balance)
    {
        base.Display();
        _priceView.Display(price.ToString());
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
