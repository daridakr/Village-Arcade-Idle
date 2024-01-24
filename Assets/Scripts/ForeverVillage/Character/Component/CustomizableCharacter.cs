using UnityEngine;

namespace ForeverVillage.Scripts.Character
{
    public class CustomizableCharacter : MonoBehaviour
    {
        [SerializeField] private Renderer[] _bodyRenderers;
        [SerializeField] private Transform _headRig;
        [SerializeField] private Transform _handRig;
        [SerializeField] private MeshFilter _head;

        public Renderer[] Body => _bodyRenderers;
        public Transform HeadRig => _headRig;
        public Transform HandRig => _handRig;
        public MeshFilter Head => _head;
    }
}