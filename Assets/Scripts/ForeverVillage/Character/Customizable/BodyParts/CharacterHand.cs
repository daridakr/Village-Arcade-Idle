using UnityEngine;

namespace Village.Character
{
    [RequireComponent(typeof(Transform))]
    public sealed class CharacterHand : MonoBehaviour
    {
        [SerializeField] private bool _main;

        private Transform _rig;

        public Transform Rig => _rig;
        public bool IsMain => _main;

        public void Initialize() => _rig = GetComponent<Transform>();
    }
}