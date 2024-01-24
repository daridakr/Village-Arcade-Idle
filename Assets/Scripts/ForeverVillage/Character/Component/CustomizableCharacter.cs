using System.Collections.Generic;
using UnityEngine;

namespace ForeverVillage.Scripts.Character
{
    public class CustomizableCharacter : MonoBehaviour
    {
        private CharacterBody _body;
        private CharacterHead _head;
        private CharacterHand _hand;

        private List<Renderer> _skinRenderers;
        private MeshFilter _headMesh;
        private Transform _headRig;
        private Transform _handRig;

        public Renderer[] SkinRenderers => _skinRenderers.ToArray();
        public MeshFilter HeadMesh => _headMesh;
        public Transform HeadRig => _headRig;
        public Transform HandRig => _handRig;

        private void Awake()
        {
            _skinRenderers = new List<Renderer>();

            _body = GetComponentInChildren<CharacterBody>();
            _body.Initialize();

            _head = GetComponentInChildren<CharacterHead>();
            _head.Initialize();
            _headMesh = _head.Mesh;
            _headRig = _head.Rig;

            _hand = GetComponentInChildren<CharacterHand>();
            _hand.Initialize();
            _handRig = _hand.Rig;

            if (_head.IsSkin) _skinRenderers.Add(_head.Renderer);
            if (_body.IsSkin) _skinRenderers.Add(_body.Renderer);
        }
    }
}