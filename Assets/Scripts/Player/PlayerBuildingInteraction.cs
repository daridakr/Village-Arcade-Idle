using UnityEngine;

public class PlayerBuildingInteraction : MonoBehaviour
{
    [SerializeField] private BuildingInteractor[] _buildingInteractions;
    [SerializeField] private InputSwitcher _inputSwitcher;

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
        _inputSwitcher.Disable();
    }

    private void OnStopped()
    {
        _inputSwitcher.Enable();
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
