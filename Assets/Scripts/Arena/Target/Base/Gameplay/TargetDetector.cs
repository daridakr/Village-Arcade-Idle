using System;
using System.Collections.Generic;
using UnityEngine;

namespace Arena
{
    public abstract class TargetDetector : MonoBehaviour,
        ITargetsInfo
    {
        protected List<Target> _targets;

        public ITargetable[] All => _targets.ToArray();
        public Target Nearest => _targets[0];

        public bool IsTargetDetected => _targets.Count > 0;

        public event Action Changed;
        public event Action OnNoneTarget;

        protected virtual void Awake() => _targets = new List<Target>();

        protected virtual void OnEnable() => OnNoneTarget?.Invoke();

        private void Update()
        {
            if (_targets.Count > 0)
                UpdateTargetsDistance();
        }

        private void UpdateTargetsDistance()
        {
            foreach (var target in _targets)
            {
                if (target == null) _targets.Remove(target);
                target.SetDistanceTo(transform.position);
            }
        }

        protected void AddTarget(Target target)
        {
            _targets.Add(target);
            target.Inactived += RemoveTarget;
            SortTargetsByDistance();

            Changed?.Invoke();
        }

        protected void RemoveTarget(Target target)
        {
            _targets.Remove(target);
            target.Inactived -= RemoveTarget;

            if (_targets.Count > 0)
                SortTargetsByDistance();
            else
                OnNoneTarget?.Invoke();

            Changed?.Invoke();
        }

        private void SortTargetsByDistance() => _targets.Sort((t1, t2) => t1.Distance.CompareTo(t2.Distance));
    }
}