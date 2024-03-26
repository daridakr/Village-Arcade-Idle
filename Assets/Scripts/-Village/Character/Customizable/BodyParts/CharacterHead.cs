using UnityEngine;

namespace Village.Character
{
    [RequireComponent(typeof(Renderer))]
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(Transform))]
    public sealed class CharacterHead : MonoBehaviour
    {
        [SerializeField] private bool _isSkin = true;

        private Renderer _renderer;
        private MeshFilter _mesh;
        private Transform _rig;

        public Renderer Renderer => _renderer;
        public MeshFilter Mesh => _mesh;
        public Transform Rig => _rig;
        public bool IsSkin => _isSkin;

        public void Initialize()
        {
            _renderer = GetComponent<Renderer>();
            _mesh = GetComponent<MeshFilter>();
            _rig = GetComponent<Transform>();
        }
    }
}