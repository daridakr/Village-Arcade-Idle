using UnityEngine;

namespace Village.Character
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(Renderer))]
    public sealed class CharacterBeard : MonoBehaviour
    {
        private MeshFilter _mesh;
        private Renderer _renderer;

        public MeshFilter Mesh => _mesh;
        public Renderer Renderer => _renderer;

        public void Initialize()
        {
            _mesh = GetComponent<MeshFilter>();
            _renderer = GetComponent<Renderer>();
        }
    }
}