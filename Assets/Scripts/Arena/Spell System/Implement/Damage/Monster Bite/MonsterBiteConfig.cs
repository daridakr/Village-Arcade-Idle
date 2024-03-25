using UnityEngine;

namespace Arena
{
    [CreateAssetMenu(fileName = "MonsterBiteSpellConfig", menuName = "Spells/Damage/Monster Bite")]
    public sealed class MonsterBiteConfig : DamageSpellConfig
    {
        public override Spell InstantiateSpell() => new MonsterBite(this);
    }
}