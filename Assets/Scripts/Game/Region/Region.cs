using System;
using UnityEngine;

public class Region : MonoBehaviour
{
    [SerializeField] private int _price;
    [SerializeField] private MeshRenderer _lockRenderer;
    [SerializeField] private int _requiredLevel; // need to define spec unlock required class
    [SerializeField] private PlayerLevel _playerLevel;

    public event Action<int> Locked;
    public event Action<int> Unlocked;
    public event Action Buyed;

    private bool _locked;

    private void OnEnable()
    {
        _playerLevel.LevelChanged += OnPlayerLevelChanged;
    }

    private void Start()
    {
        if (_locked)
        {
            Locked?.Invoke(_requiredLevel);
        }
    }

    private void OnPlayerLevelChanged(int level)
    {
        if (level >= _requiredLevel) // should somehow indefy if is region already buyed
        {
            Unlocked?.Invoke(_price);
            _locked = false;
            _playerLevel.LevelChanged -= OnPlayerLevelChanged;
        }
        else
        {
            _locked = true;
        }
    }
}
