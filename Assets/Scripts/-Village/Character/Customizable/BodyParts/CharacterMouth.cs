using UnityEngine;

namespace Village.Character
{
    [RequireComponent(typeof(MeshFilter))]
    public class CharacterMouth : MonoBehaviour
    {
        private MeshFilter _mesh;
        public MeshFilter Mesh => _mesh;
        public void Initialize()
        {
            _mesh = GetComponent<MeshFilter>();
        }
    }
}