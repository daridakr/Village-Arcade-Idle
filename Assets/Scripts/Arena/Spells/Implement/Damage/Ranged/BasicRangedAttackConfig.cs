using UnityEngine;

namespace Arena
{
    [CreateAssetMenu(fileName = "BasicRangedAttackConfig", menuName = "Spells/Damage/Basic Ranged")]
    public class BasicRangedAttackConfig : DamageSpellConfig
    {
        [SerializeField] private GameObject _projectile;
        [SerializeField][Min(0)] private int _count;
        [SerializeField][Min(0.1f)] private float _speed;

        public GameObject Projectile => _projectile;
        public int Count => _count;
        public float Speed => _speed;
    }
}