using UnityEngine;

namespace Arena
{
    [CreateAssetMenu(fileName = "ShiledProtectSpellConfig", menuName = "Spells/Damage/Shiled Protect")]
    public sealed class ShieldProtectConfig : BonusSpellConfig
    {
        public override Spell InstantiateSpell() => new ShieldProtect(this);
    }
}