using UnityEngine;

namespace Village.Player
{
    [CreateAssetMenu(fileName = "MovementConfig", menuName = "Gameplay/Movement Config")]
    public sealed class MovementConfig : ScriptableObject
    {
        [SerializeField][Range(100, 600)] private float _speed = 250f;
        [SerializeField][Range(360, 720)] private float _maxRotationSpeed = 360f;

        public float Speed => _speed;
        public float MaxRotationSpeed => _maxRotationSpeed;
    }
}