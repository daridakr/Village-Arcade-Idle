using System;
using UnityEngine;
using Zenject;

public class BuildingZoneView : MonoBehaviour
{
    [SerializeField] private ClearBuildingZoneCanvas _clearCanvas;
    [SerializeField] private SetBuildingCanvas _buildCanvas;
    [SerializeField] private BuildingListView _buildingListView;
    [SerializeField] private SpriteRenderer _dottedSquare;

    [SerializeField] private ParticleSystem _freeEffect;
    [SerializeField] private ParticleSystem _buildEffect;

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
        _clearCanvas.ClearButtonClicked += OnClearZone;
        _buildCanvas.BuildButtonClicked += OnShowBuildings;
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

    private void OnClearZone()
    {
        _freeEffect.Stop();
        CanClear?.Invoke();
    }

    private void OnShowBuildings()
    {
        _buildingListView.Display();
        _buildingListView.OnSmthBuyed += OnBuildZone;
    }

    private void OnBuildZone(BuildingData data)
    {
        _buildCanvas.Hide();
        _buildingListView.OnSmthBuyed -= OnBuildZone;

        _dottedSquare.enabled = false;
        _buildEffect.Play();

        CanBuild?.Invoke(data);
        Invoke(nameof(StopEffect), 2f);
    }

    private void StopEffect()
    {
        _buildEffect.Stop();
    }

    private void OnDisable()
    {
        _clearCanvas.ClearButtonClicked -= OnClearZone;
        _buildCanvas.BuildButtonClicked -= OnShowBuildings;
    }
}
