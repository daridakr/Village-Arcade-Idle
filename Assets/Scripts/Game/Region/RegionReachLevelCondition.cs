using System;
using UnityEngine;

// maybe should also define separate base class for reach conditions
[RequireComponent(typeof(ReachableRegion))]
public class RegionReachLevelCondition : MonoBehaviour, IRegionReachCondition
{
    [SerializeField] private PlayerLevel _playerLevel;
    [SerializeField] private int _requiredLevel;

    public int Condition => _requiredLevel;
    public bool IsCompleted { get; private set; }

    public event Action Completed;

    private void OnEnable()
    {
        _playerLevel.LevelChanged += OnPlayerLevelChanged;
    }

    private void OnPlayerLevelChanged(int level)
    {
        if (level >= Condition)
        {
            Complete();
        }
    }

    private void Complete()
    {
        IsCompleted = true;
        Destroy(this);
        Completed?.Invoke();
        _playerLevel.LevelChanged -= OnPlayerLevelChanged;
        //_unlockable.Unlock(transform, onLoad, GUID);
    }
}
