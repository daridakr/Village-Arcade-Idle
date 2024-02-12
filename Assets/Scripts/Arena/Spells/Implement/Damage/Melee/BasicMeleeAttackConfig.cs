using UnityEngine;

namespace Arena
{
    [CreateAssetMenu(fileName = "BasicMeleeAttackConfig", menuName = "Spells/Damage/Basic Melee")]
    public class BasicMeleeAttackConfig : DamageSpellConfig
    {
        [SerializeField] private float _range = 1.5f;
        [SerializeField][Min(0.1f)] private float _speed = 1.5f;

        public float Range => _range;
        public float Speed => _speed;

        protected override void Validate()
        {
            base.Validate();

            _castingTime = 0;
            _lifeTime = 0;
        }
    }
}