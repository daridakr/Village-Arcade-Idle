using System;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    [SerializeField] private BuildingData _data;
    [SerializeField] private BuildingLevel[] _levels;

    [SerializeField] protected int _countInOneRegion;
    [SerializeField] protected int _maxLevel;

    private Builded _builded;

    private int _level = 0;
    private BuildingLevel _currentLevel;
    protected float _upgradeMultiplier;
    // unlock type (blueprint, quest, level)

    public BuildingData Data => _data;

    public event Action Upgraded;

    private void Awake()
    {
        _currentLevel = _levels[_level];

        foreach (var level in _levels)
        {
            level.gameObject.SetActive(level != _currentLevel ? false : true);
        }
    }

    // virtual?
    public virtual void Upgrade()
    {
        _level++;
        Upgraded?.Invoke();
    }

    public virtual void Destroy()
    {
        Destroy(gameObject);
    }
}
