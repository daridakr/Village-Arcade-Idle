using UnityEngine;

namespace ForeverVillage.Scripts.Character
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(Renderer))]
    public class CharacterEyebrows : MonoBehaviour
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