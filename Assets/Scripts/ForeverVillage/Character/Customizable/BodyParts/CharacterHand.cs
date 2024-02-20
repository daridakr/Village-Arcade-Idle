using UnityEngine;

namespace Village.Character
{
    [RequireComponent(typeof(Transform))]
    public sealed class CharacterHand : MonoBehaviour
    {
        private Transform _rig;
        public Transform Rig => _rig;
        public void Initialize()
        {
            _rig = GetComponent<Transform>();
        }
    }
}