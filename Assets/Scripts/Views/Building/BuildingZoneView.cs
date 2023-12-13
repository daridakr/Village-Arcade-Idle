using System;
using UnityEngine;
using Zenject;

public class BuildingZoneView : MonoBehaviour
{
    [SerializeField] private MessageDisplayCanvas _clearCanvas;
    [SerializeField] private ButtonCanvas _buildCanvas;

    private BuildingListView _buildingListView;
    private CanvasAnimatedView _currentCanvas;

    public event Action CanClear;
    public event Action<BuildingData> CanBuild;

    [Inject]
    public void Construct(BuildingListView buildingsList)
    {
        _buildingListView = buildingsList;
    }

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
        _buildingListView.Display();
        _buildingListView.OnSmthBuyed += OnBuildZone;
    }

    private void OnBuildZone(BuildingData data)
    {
        _buildCanvas.Hide();
        _buildingListView.OnSmthBuyed -= OnBuildZone;

        CanBuild?.Invoke(data);
    }

    private void OnDisable()
    {
        _clearCanvas.ButtonClicked -= OnClearZone;
        _buildCanvas.ButtonClicked -= OnShowBuildings;
    }
}
