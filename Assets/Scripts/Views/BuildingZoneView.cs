using System;
using UnityEngine;

public class BuildingZoneView : MonoBehaviour
{
    [SerializeField] private ClearZoneCanvas _clearCanvas;

    public event Action ClearStarted;

    private void OnEnable()
    {
        _clearCanvas.ClearButtonClicked += OnClearZone;
    }

    public void ShowClearCanvas(int clearPrice, int balance)
    {
        _clearCanvas.Display(clearPrice, balance);
    }

    public void HideClearCanvas()
    {
        _clearCanvas.Hide();
    }

    public void ShowBuildCanvas()
    {

    }

    public void ShowUpgradeCanvas()
    {

    }

    public void ShowCircleTimer()
    {

    }

    private void OnClearZone()
    {
        // show new canvas circle timer circle
        ShowCircleTimer();

        ClearStarted?.Invoke();
    }
}
