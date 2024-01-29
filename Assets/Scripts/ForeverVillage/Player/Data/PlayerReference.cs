using UnityEngine;

namespace Village
{
    public sealed class PlayerReference : global::PlayerReference
    {
        [SerializeField] private GameObject _interactorComponents;
        [SerializeField] private UpgradablePlayerMovement _movement;

        public GameObject Interactors => _interactorComponents;
        public Vector3 Position => _movement.CurrentPosition;
    }
}