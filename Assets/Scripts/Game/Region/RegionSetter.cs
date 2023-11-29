using UnityEngine;

public class RegionSetter : MonoBehaviour
{
    [SerializeField] private BuildingZone _buildingZoneTempalte;
    [SerializeField] private Transform[] _buildingZonePoints;

    private void Start()
    {
        foreach (Transform transform in _buildingZonePoints)
        {
            Instantiate(_buildingZoneTempalte, transform);
        }
    }
}
