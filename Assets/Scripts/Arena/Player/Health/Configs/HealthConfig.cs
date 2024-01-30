using UnityEngine;

namespace Vampire
{
    [CreateAssetMenu(fileName = "NewHealthConfig", menuName = "Gameplay/Health Config")]
    public sealed class HealthConfig : ScriptableObject
    {
        [SerializeField] private float _minValue = 0f;
        [SerializeField] private float _maxValue = 100f;

        public float Min => _minValue;
        public float Max => _maxValue;
    }
}