using UnityEngine;
using Zenject;

public class PlayerBuildingInteraction : MonoBehaviour
{
    [SerializeField] private BuildingInteractor[] _buildingInteractions;

    private IInputService _inputService;

    [Inject]
    public void Construct(IInputService inputService)
    {
        _inputService = inputService;
    }

    private void OnEnable()
    {
        foreach (BuildingInteractor interaction in _buildingInteractions)
        {
            interaction.Started += OnStarted;
            interaction.Stopped += OnStopped;
        }
    }

    private void OnStarted()
    {
        _inputService.Disable();
    }

    private void OnStopped()
    {
        _inputService.Enable();
    }

    private void OnDisable()
    {
        foreach (BuildingInteractor interaction in _buildingInteractions)
        {
            interaction.Started -= OnStarted;
            interaction.Stopped -= OnStopped;
        }
    }
}
