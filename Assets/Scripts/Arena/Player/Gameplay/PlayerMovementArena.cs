using UnityEngine;

namespace Arena
{
    public sealed class PlayerMovementArena : PlayerMovement
    {
        [SerializeField] private TargetDetector _targetDetector;

        private Target _currentTarget;

        private void OnEnable() => _targetDetector.OnNoneTarget += OnNoneTarget;

        private void FixedUpdate()
        {
            if (!_targetDetector)
                return;

            if (_targetDetector.IsTargetDetected)
                LookAtTarget();
        }

        private void LookAtTarget()
        {
            _currentTarget = _targetDetector.Nearest;

            Vector3 targetPosition = _currentTarget.transform.position;
            Vector3 directionToTarget = (targetPosition - transform.position).normalized;
            directionToTarget.y = 0f;

            _isMovingRotation = false;
            UpdateRotation(directionToTarget);
        }

        private void OnNoneTarget()
        {
            _isMovingRotation = true;
            _currentTarget = null;
        } 

        private void OnDisable() => _targetDetector.OnNoneTarget -= OnNoneTarget;
    }
}