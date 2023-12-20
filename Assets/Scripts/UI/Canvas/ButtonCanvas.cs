using System;
using UnityEngine;

public class ButtonCanvas : CanvasAnimatedView
{
    [SerializeField] protected ButtonDisplay _buttonDisplay;

    public event Action ButtonClicked;

    public void Display(bool interactable = true)
    {
        base.Display();
        _buttonDisplay.Clicked += OnButtonClicked;
        _buttonDisplay.SetInteractable(interactable);
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

