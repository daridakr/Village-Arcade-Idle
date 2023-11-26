using System;
using UnityEngine;

public class SetBuildingCanvas : CanvasAnimatedView
{
    [SerializeField] private PriceButtonView _buildButton; // should define to common interface for canvas with button and according click event

    public event Action BuildButtonClicked;

    private void OnEnable()
    {
        _buildButton.Clicked += OnBuildButtonClicked;
    }

    private void OnBuildButtonClicked()
    {
        Hide();
        BuildButtonClicked?.Invoke();
    }

    private void OnDisable()
    {
        _buildButton.Clicked -= OnBuildButtonClicked;
    }
}
