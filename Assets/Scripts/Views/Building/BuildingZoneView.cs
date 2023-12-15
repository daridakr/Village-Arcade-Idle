using System;
using UnityEngine;

public class BuildingZoneView : MonoBehaviour
{
    [SerializeField] private MessageDisplayCanvas _clearCanvas;
    [SerializeField] private ButtonCanvas _buildCanvas;
    [SerializeField] private ButtonCanvas _upgradeCanvas;
    [SerializeField] private BuildingsStoreDisplay _buildingsStore;

    private CanvasAnimatedView _currentCanvas;

    public event Action CanClear;
    public event Action<Building> CanBuild;

    private void OnEnable()
    {
        _clearCanvas.ButtonClicked += OnClearZone;
        _buildCanvas.ButtonClicked += OnShowBuildings;
    }

    public void ShowClearCanvas(int clearPrice, int balance)
    {
        _clearCanvas.Display(clearPrice, balance >= clearPrice);
        _currentCanvas = _clearCanvas;
    }

    public void ShowBuildCanvas()
    {
        _buildCanvas.Display();
        _currentCanvas = _buildCanvas;
    }

    public void ShowUpgradeCanvas()
    {
        _upgradeCanvas.Display();
        _currentCanvas = _upgradeCanvas;
    }

    public void HideCanvas()
    {
        _currentCanvas.Hide();
    }

    private void OnClearZone()
    {
        _clearCanvas.Hide();
        CanClear?.Invoke();
    }

    private void OnShowBuildings()
    {
        _buildCanvas.Hide();
        _buildingsStore.Display();
        _buildingsStore.OnSmthBuyed += OnBuildZone;
    }

    private void OnBuildZone(Building building)
    {
        _buildCanvas.Hide();
        _buildingsStore.OnSmthBuyed -= OnBuildZone;

        CanBuild?.Invoke(building);
    }

    private void OnDisable()
    {
        _clearCanvas.ButtonClicked -= OnClearZone;
        _buildCanvas.ButtonClicked -= OnShowBuildings;
    }
}
