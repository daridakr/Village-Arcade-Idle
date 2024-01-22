using UnityEngine;

namespace ForeverVillage.Scripts.Character
{
    public class CustomizableCharacter : MonoBehaviour
    {
        [SerializeField] private Renderer[] _bodyRenderers;
        [SerializeField] private Transform _headRig;
        [SerializeField] private MeshFilter _head;

        public Renderer[] Body => _bodyRenderers;
        public Transform HeadRig => _headRig;
        public MeshFilter Head => _head;
    }
}