using System;
using UnityEngine;

public class SetBuildingCanvas : CanvasAnimatedView
{
    // should define to common interface for canvas with button and according click event
    [SerializeField] private PriceButtonView _buildButton; 

    public event Action BuildButtonClicked;

    private void OnEnable()
    {
        _buildButton.Clicked += OnBuildButtonClicked;
    }

    private void OnBuildButtonClicked()
    {
        BuildButtonClicked?.Invoke();
    }

    private void OnDisable()
    {
        _buildButton.Clicked -= OnBuildButtonClicked;
    }
}
