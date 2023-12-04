using UnityEngine;

public class Region : MonoBehaviour
{
    [SerializeField] private int _price;
    [SerializeField] private int _requiredLevel; // need to define spec unlock required class
    [SerializeField] private RegionZone _zone;
    [SerializeField] private MeshRenderer _lockRenderer;
    [SerializeField] private GameObject _data;
    [SerializeField] private PlayerLevel _playerLevel;

    private bool _locked;

    private void OnEnable()
    {
        _playerLevel.LevelChanged += OnPlayerLevelChanged;
    }

    private void Start()
    {
        if (_locked)
        {
            _zone.Lock(_requiredLevel);
        }
    }

    private void OnPlayerLevelChanged(int level)
    {
        if (level >= _requiredLevel) // should somehow indefy if is region already buyed
        {
            //_unlockable.Unlock(transform, onLoad, GUID);
            _locked = false;
            _zone.Unlock(_price);
            _zone.Buyed += OnRegionBuyed;
            _playerLevel.LevelChanged -= OnPlayerLevelChanged;
        }
        else
        {
            _locked = true;
        }
    }

    private void OnRegionBuyed()
    {
        _zone.Buyed -= OnRegionBuyed;
        Destroy(_lockRenderer.gameObject);
        _data.SetActive(true);
    }
}
