using System;
using UnityEngine;

[RequireComponent(typeof(GuidableObject))]
public abstract class Building : MonoBehaviour
{
    [SerializeField] private BuildingData _data;
    [SerializeField] private BuildingLevel[] _levels;

    [SerializeField] protected int _countInOneRegion;
    [SerializeField] protected int _maxLevel;

    private BuildedBuilding _model;
    //need unique guid for building and also guid for builded building
    private GuidableObject _guidable;

    private int _level = 0;
    private BuildingLevel _currentLevel;
    protected float _upgradeMultiplier;
    // unlock type (blueprint, quest, level)

    public BuildingData Data => _data;
    public string Guid => _guidable.GUID;

    public event Action Upgraded;

    private void OnEnable()
    {
        _guidable = GetComponent<GuidableObject>();
        _guidable.RegenerateGUID();
        //_model.Destroyed += OnDestroyedZone;
        //_model.Cleared += OnClearedZone;
        //_model.Builded += OnBuildedZone;
    }

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
