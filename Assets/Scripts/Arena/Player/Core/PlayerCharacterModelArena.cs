using UnityEngine;

namespace Arena
{
    public sealed class PlayerCharacterModelArena : PlayerCharacterModel
    {
        [SerializeField] private TargetDetector _targetDetector;
        [SerializeField] private float _rotationSpeed = 5f;

        private Target _currentTarget;
        private Vector3 _currentDirection;

        private void OnEnable()
        {
            _targetDetector.OnNoneTarget += OnNoneTarget;
            //_targetDetector.Changed += LookAtTarget;
        }

        private void Update()
        {
            if (_targetDetector)
            {
                if (_targetDetector.IsTargetDetected)
                {
                    StopListenMovementDirection();
                    UpdateLookDirection();
                }
            }
        }

        private void UpdateLookDirection()
        {
            _currentTarget = _targetDetector.Nearest;

            Vector3 targetPosition = _currentTarget.transform.position;
            Vector3 directionToTarget = (targetPosition - transform.position).normalized;
            directionToTarget.y = 0f;

            _currentDirection = Vector3.Lerp(_currentDirection, directionToTarget, Time.deltaTime * _rotationSpeed);
            UpdateRotation(_currentDirection);
        }

        private void OnNoneTarget()
        {
            _currentTarget = null;

            StartListenMovementDirection();
        }

        private void OnDisable()
        {
            _targetDetector.OnNoneTarget -= OnNoneTarget;
            //_targetDetector.Changed -= LookAtTarget;
        }
    }
}