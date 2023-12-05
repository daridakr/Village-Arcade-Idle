using UnityEngine;

public class RegionLock : MonoBehaviour
{
    [SerializeField] private RegionData _region;
    [SerializeField] private RegionZone _interactionZone;

    private void OnEnable()
    {
        _interactionZone.Init(_region.Price);
        _interactionZone.Paid += OnRegionPaid;
    }

    private void Start()
    {
        _region.Hide();
    }

    private void OnRegionPaid()
    {
        _region.Display();
        _interactionZone.Paid -= OnRegionPaid;
        Destroy(_interactionZone.gameObject);
        Destroy(gameObject);
    }
}
