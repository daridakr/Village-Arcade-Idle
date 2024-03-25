using UnityEngine;

namespace Arena
{
    [CreateAssetMenu(fileName = "SwordStrokeSpellConfig", menuName = "Spells/Damage/Sword Stroke")]
    public sealed class SwordStrokeConfig : DamageSpellConfig
    {
        [SerializeField] private SpellEffect _effect;
        [SerializeField][Range(50, 300)] private float _knockbackForce = 50f;

        public float KnockbackForce => _knockbackForce;
        public SpellEffect Effect => _effect;

        public override Spell InstantiateSpell() => new SwordStroke(this);

        protected override void Validate()
        {
            base.Validate();

            _castingTime = 0;
            _lifeTime = 0;
        }
    }
}