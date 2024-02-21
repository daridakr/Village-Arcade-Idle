using UnityEngine;

namespace Village.Player
{
    [CreateAssetMenu(fileName = "MovementConfig", menuName = "Gameplay/Movement Config")]
    public sealed class MovementConfig : ScriptableObject
    {
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _speedRate = 1f;
        [SerializeField] private float _flySpeedRate = 1f;
        public float Speed => _speed;
        public float SpeedRate => _speedRate;
        public float FlySpeedRate => _flySpeedRate;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_speed <= 0f)
                _speed = 1f;

            if (_speedRate <= 0f)
                _speedRate = 1f;

            if (_flySpeedRate <= 0f)
                _flySpeedRate = 1f;
        }
#endif
    }
}