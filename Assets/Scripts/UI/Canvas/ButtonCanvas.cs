using System;
using UnityEngine;

public class ButtonCanvas : CanvasAnimatedView
{
    [SerializeField] protected ButtonDisplay _buttonDisplay;

    public event Action ButtonClicked;

    public override void Display()
    {
        base.Display();
        _buttonDisplay.Clicked += OnButtonClicked;
    }

    private void OnButtonClicked()
    {
        ButtonClicked?.Invoke();
    }

    public override void Hide()
    {
        base.Hide();
        _buttonDisplay.Clicked -= OnButtonClicked;
    }
}

