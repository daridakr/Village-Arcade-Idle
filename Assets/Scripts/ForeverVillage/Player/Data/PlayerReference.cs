using UnityEngine;

namespace Village
{
    public sealed class PlayerReference : global::PlayerReference
    {
        [SerializeField] private GameObject _interactorComponents;
        [SerializeField] private PlayerMovementUpgradable _movement;

        public GameObject Interactors => _interactorComponents;
        public Vector3 LastPosition => _movement.CurrentPosition;
    }
}