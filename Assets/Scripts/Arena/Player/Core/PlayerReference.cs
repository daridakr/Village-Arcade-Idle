using ForeverVillage;
using UnityEngine;

namespace Arena
{
    public sealed class PlayerReference : PlayerReferenceBase
    {
        // specializations
        public GameObject[] startingAbilities;

        // stats

        [SerializeField] private Collider collectableCollider;
        private Rigidbody _body;

        public Collider CollectableCollider { get => collectableCollider; }

        //public void Init(EntityManager entityManager, AbilityManager abilityManager, StatsManager statsManager)
        //{

        //}
    }
}