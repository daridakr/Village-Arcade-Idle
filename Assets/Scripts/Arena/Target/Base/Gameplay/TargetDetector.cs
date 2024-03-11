using System;
using System.Collections.Generic;
using UnityEngine;

namespace Arena
{
    [RequireComponent(typeof(Collider))]
    public class TargetDetector : MonoBehaviour,
        ITargetsInfo
    {
        [SerializeField] private TargetTrigger _trigger;

        private List<Target> _targets;

        public ITargetable[] All => _targets.ToArray();
        public Target Nearest => _targets[0];

        public bool IsTargetDetected => _targets.Count > 0;

        public event Action Changed;
        public event Action OnNoneTarget;

        private void OnEnable()
        {
            _trigger.Enter += OnTargetEnter;
            _trigger.Exit += OnTargetExit;

            OnNoneTarget?.Invoke();
        }

        private void Awake()
        {
            _targets = new List<Target>();
        }

        private void Update()
        {
            if (_targets.Count > 0)
                UpdateTargetsDistance();
        }

        private void UpdateTargetsDistance()
        {
            foreach (var target in _targets)
            {
                target.SetDistanceTo(transform.position);
            }
        }

        private void OnTargetEnter(Target target)
        {
            if (target == null)
                return;

            Target existingTarget = _targets.Find(t => t == target);

            if (existingTarget == null)
                AddTarget(target);
        }

        private void OnTargetExit(Target target)
        {
            Target existingTarget = _targets.Find(t => t == target);

            if (existingTarget != null)
                RemoveTarget(target);
        }

        private void AddTarget(Target target)
        {
            _targets.Add(target);
            target.Deslocated += RemoveTarget;
            SortTargetsByDistance();

            Changed?.Invoke();
        }

        private void RemoveTarget(Target target)
        {
            _targets.Remove(target);
            target.Deslocated -= RemoveTarget;

            if (_targets.Count > 0)
                SortTargetsByDistance();
            else
                OnNoneTarget?.Invoke();

            Changed?.Invoke();
        }

        private void SortTargetsByDistance() => _targets.Sort((t1, t2) => t1.Distance.CompareTo(t2.Distance));

        private void OnDisable()
        {
            _trigger.Enter -= OnTargetEnter;
            _trigger.Exit -= OnTargetExit;
        }
    }
}