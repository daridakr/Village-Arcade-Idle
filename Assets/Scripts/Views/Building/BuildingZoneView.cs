using System;
using UnityEngine;
using Zenject;

public class BuildingZoneView : MonoBehaviour
{
    [SerializeField] private BuildingZone _zone;
    [SerializeField] private MessageDisplayCanvas _clearCanvas;
    [SerializeField] private ButtonCanvas _buildCanvas;
    [SerializeField] private SpriteRenderer _dottedSquare;
    [SerializeField] private ParticleSystem _freeEffect;
    [SerializeField] private ParticleSystem _buildEffect;

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
        _freeEffect.Stop();
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
        _clearCanvas.ButtonClicked -= OnClearZone;
        _buildCanvas.ButtonClicked -= OnShowBuildings;
    }
}
