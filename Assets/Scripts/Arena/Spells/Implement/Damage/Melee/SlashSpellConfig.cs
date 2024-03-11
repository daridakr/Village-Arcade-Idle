using UnityEngine;

namespace Arena
{
    [CreateAssetMenu(fileName = "BasicMeleeAttackConfig", menuName = "Spells/Damage/Basic Melee")]
    public class SlashSpellConfig : DamageSpellConfig
    {
        [SerializeField] private float _range = 1.5f;
        [SerializeField] private float _slashTime;
        [SerializeField][Min(0.1f)] private float _speed = 1.5f;

        public float Range => _range;
        public float Speed => _speed;
        public float SlashTime => _slashTime;

        public override Spell InstantiateSpell() => new SlashSpell(this);

        protected override void Validate()
        {
            base.Validate();

            _castingTime = 0;
            _lifeTime = 0;
        }
    }
}