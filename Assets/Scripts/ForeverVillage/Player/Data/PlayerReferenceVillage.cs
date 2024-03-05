using ForeverVillage;
using UnityEngine;

namespace Village
{
    public sealed class PlayerReferenceVillage : PlayerReferenceBase
    {
        [SerializeField] private GameObject _interactorComponents;
        [SerializeField] private PlayerMovementUpgradable _movement;

        public GameObject Interactors => _interactorComponents;
        public Vector3 LastPosition => _movement.CurrentPosition;
    }
}