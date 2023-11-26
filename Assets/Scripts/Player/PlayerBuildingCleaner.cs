using UnityEngine;

public class PlayerBuildingCleaner : MonoBehaviour
{
    [SerializeField] private BuildingCleaner _buildingCleaner;
    [SerializeField] private InputSwitcher _inputSwitcher;

    private void OnEnable()
    {
        _buildingCleaner.StartedCleaning += OnClean;
        _buildingCleaner.StoppedCleaning += OnCleaned;
    }

    private void OnClean()
    {
        _inputSwitcher.Disable();
    }

    private void OnCleaned()
    {
        _inputSwitcher.Enable();
    }

    private void OnDisable()
    {
        _buildingCleaner.StartedCleaning -= OnClean;
        _buildingCleaner.StoppedCleaning -= OnCleaned;
    }
}
