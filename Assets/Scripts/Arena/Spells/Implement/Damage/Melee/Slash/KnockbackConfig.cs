using UnityEngine;

namespace Arena
{
    [CreateAssetMenu(fileName = "KnockbackConfig", menuName = "Spells/Damage/Melee/Slash", order = 8)]
    public sealed class KnockbackConfig : ScriptableObject
    {
        [SerializeField] private float _strength = 25000;
        [SerializeField] private ParticleSystem.MinMaxCurve _distanceFalloff;

        public Vector3 GetKnockbackStrength(Vector3 direction, float distance) =>
            _strength * _distanceFalloff.Evaluate(distance) * direction;
    }
}