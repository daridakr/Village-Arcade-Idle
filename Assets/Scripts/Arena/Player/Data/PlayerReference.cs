using UnityEngine;

namespace Vampire
{
    public sealed class PlayerReference : global::PlayerReference
    {
        // specializations
        public GameObject[] startingAbilities;

        // stats

        [SerializeField] private Collider collectableCollider;
        private Rigidbody _body;

        public Collider CollectableCollider { get => collectableCollider; }

        public void Init(EntityManager entityManager, AbilityManager abilityManager, StatsManager statsManager)
        {

        }
    }
}