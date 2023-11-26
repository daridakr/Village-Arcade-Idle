using System;
using UnityEngine;

public class BuildingZoneView : MonoBehaviour
{
    [SerializeField] private ClearZoneCanvas _clearCanvas;
    [SerializeField] private SetBuildingCanvas _buildCanvas;
    [SerializeField] private BuildingListView _buildingListView;
    [SerializeField] private SpriteRenderer _dottedSquare;

    private CanvasAnimatedView _currentCanvas;

    public event Action ClearStarted;
    public event Action<Building> BuildStarted;

    private void OnEnable()
    {
        _clearCanvas.ClearButtonClicked += OnClearZone;
        _buildCanvas.BuildButtonClicked += OnShowBuildings;
        _buildingListView.OnBuyed += OnBuildZone;
    }

    public void ShowClearCanvas(int clearPrice, int balance)
    {
        _clearCanvas.Display(clearPrice, balance);
        _currentCanvas = _clearCanvas;
    }

    public void ShowBuildCanvas()
    {
        _buildCanvas.Display();
        _currentCanvas = _buildCanvas;
    }

    public void HideCanvas()
    {
        _currentCanvas.Hide();
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

    private void OnShowBuildings()
    {
        _buildingListView.Display();
    }

    private void OnBuildZone(BuildingListItemView building)
    {
        _dottedSquare.enabled = false;
        BuildStarted?.Invoke(building.BuildingData);
    }

    private void OnDisable()
    {
        _clearCanvas.ClearButtonClicked -= OnClearZone;
        _buildCanvas.BuildButtonClicked -= OnShowBuildings;
        _buildingListView.OnBuyed -= OnBuildZone;
    }
}
