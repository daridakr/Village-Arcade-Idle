using System;
using UnityEngine;
using Zenject;

namespace ForeverVillage.Scripts
{
    // maybe should also define separate base class for reach conditions
    [RequireComponent(typeof(ReachableRegion))]
    public sealed class RegionReachLevelCondition : MonoBehaviour, IRegionReachCondition
    {
        [SerializeField] private int _requiredLevel;

        private ReachableRegion _reachable;
        private PlayerLevel _playerLevel;

        public int Condition => _requiredLevel;
        public bool IsCompleted { get; private set; }

        public event Action Completed;

        [Inject]
        private void Construct(PlayerLevel playerLevel)
        {
            _playerLevel = playerLevel;
        }

        private void OnEnable()
        {
            _reachable = GetComponent<ReachableRegion>();
            _reachable.Init(this);

            _playerLevel.LevelChanged += TryComplete;
        }

        private void Start()
        {
            TryComplete(_playerLevel.Level);
        }

        private void TryComplete(int level)
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
            _playerLevel.LevelChanged -= TryComplete;
            //_unlockable.Unlock(transform, onLoad, GUID);
        }
    }
}