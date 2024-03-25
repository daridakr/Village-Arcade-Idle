using UnityEngine;

namespace Arena
{
    [RequireComponent(typeof(Collider))]
    public sealed class RangeTargetDetector : TargetDetector
    {
        [SerializeField] private TargetTrigger _trigger;

        protected override void OnEnable()
        {
            base.OnEnable();

            _trigger.Enter += OnTargetEnter;
            _trigger.Exit += OnTargetExit;
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

        private void OnDisable()
        {
            _trigger.Enter -= OnTargetEnter;
            _trigger.Exit -= OnTargetExit;
        }
    }
}