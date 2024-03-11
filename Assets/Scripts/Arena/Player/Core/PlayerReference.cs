using ForeverVillage;
using UnityEngine;

namespace Arena
{
    public sealed class PlayerReference : PlayerReferenceBase
    {
        // stats

        [SerializeField] private Collider collectableCollider;
        private Rigidbody _body;

        public Collider CollectableCollider { get => collectableCollider; }

    }
}