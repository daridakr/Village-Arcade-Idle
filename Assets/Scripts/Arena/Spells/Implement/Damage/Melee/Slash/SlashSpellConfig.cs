using UnityEngine;

namespace Arena
{
    [CreateAssetMenu(fileName = "BasicMeleeAttackConfig", menuName = "Spells/Damage/Basic Melee")]
    public sealed class SlashSpellConfig : DamageSpellConfig
    {
        [SerializeField] private SpellEffect _effect;
        [SerializeField][Range(50, 300)] private float _knockbackForce;

        public float KnockbackForce => _knockbackForce;
        public SpellEffect Effect => _effect;

        public override Spell InstantiateSpell() => new SlashSpell(this);

        protected override void Validate()
        {
            base.Validate();

            _castingTime = 0;
            _lifeTime = 0;
        }
    }
}