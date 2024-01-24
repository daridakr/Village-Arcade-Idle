using UnityEngine;

namespace ForeverVillage.Scripts.Character
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