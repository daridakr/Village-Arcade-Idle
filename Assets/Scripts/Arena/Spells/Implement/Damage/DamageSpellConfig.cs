using UnityEngine;

namespace Arena
{
    public abstract class DamageSpellConfig : SpellConfig
    {
        [SerializeField][Min(0.1f)] private float _damage = 0.1f;

        public float Damage => _damage;
    }
}