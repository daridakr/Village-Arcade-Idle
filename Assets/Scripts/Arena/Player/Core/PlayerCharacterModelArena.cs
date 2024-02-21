using UnityEngine;

namespace Arena
{
    public sealed class PlayerCharacterModelArena : PlayerCharacterModel
    {
        [SerializeField] private TargetDetector _targetDetector;
        [SerializeField] private float _rotationSpeed = 5f;

        private Vector3 _currentDirection;

        private void OnEnable()
        {
            _targetDetector.OnNoneTarget += StartListenMovementDirection;
        }

        private void Update()
        {
            if (_targetDetector)
            {
                if (_targetDetector.IsTargetDetected)
                {
                    StopListenMovementDirection();
                    LookAtTarget();
                }
            }
        }

        private void LookAtTarget()
        {
            Vector3 targetPosition = _targetDetector.Nearest.transform.position;
            Vector3 directionToTarget = (targetPosition - transform.position).normalized;
            directionToTarget.y = 0f;

            _currentDirection = Vector3.Lerp(_currentDirection, directionToTarget, Time.deltaTime * _rotationSpeed);
            UpdateRotation(_currentDirection);
        }

        private void OnDisable()
        {
            _targetDetector.OnNoneTarget -= StartListenMovementDirection;
        }
    }
}