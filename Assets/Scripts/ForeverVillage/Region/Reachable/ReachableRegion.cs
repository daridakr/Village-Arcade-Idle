using System;
using UnityEngine;

namespace Village
{
    public class ReachableRegion : MonoBehaviour
    {
        private IRegionReachCondition _reachCondition;

        public event Action Reached;
        public event Action<int> Unreached;

        public void Init(IRegionReachCondition condition)
        {
            _reachCondition = condition;
        }

        //[Inject]
        //private void Construct(IRegionReachCondition condition)
        //{
        //    _reachCondition = condition;
        //}

        private void Start()
        {
            if (_reachCondition.IsCompleted)
            {
                OnRegionReached();
            }
            else
            {
                Unreached?.Invoke(_reachCondition.Condition);
                _reachCondition.Completed += OnRegionReached;
            }
        }

        private void OnRegionReached()
        {
            Reached?.Invoke();
            _reachCondition.Completed -= OnRegionReached;
            Destroy(this);
        }
    }
}