using UnityEngine;

namespace Village
{
    [CreateAssetMenu(fileName = "ItemsMagnitConfig", menuName = "Items/Items Magnit Config")]
    public sealed class ItemsMagnitConfig : ScriptableObject
    {
        [SerializeField] private float _attractDuration = 1f;
        [SerializeField] private float _speed = 5;
        [SerializeField] private float _followOffsetDistance = 5f;
        [SerializeField] private float _shakeScalingDuration = 0.2f;
        [SerializeField] private float _shakeScalingValue = 3f;
        [SerializeField] private float _scaleReduceDuration = 0.5f;
        [SerializeField] private float _scaleReduceMoveSpeed = 5;

        public float AttractDuration { get => _attractDuration; }
        public float Speed { get => _speed; }
        public float FollowOffsetDistance { get => _followOffsetDistance; }
        public float ShakeScalingDuration { get => _shakeScalingDuration; }
        public float ShakeScalingValue { get => _shakeScalingValue; }
        public float ScaleReduceDuration { get => _scaleReduceDuration; }
        public float ScaleReduceMoveSpeed { get => _scaleReduceMoveSpeed; }
    }
}