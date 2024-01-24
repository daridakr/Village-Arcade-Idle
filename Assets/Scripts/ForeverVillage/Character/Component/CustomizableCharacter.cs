using System.Collections.Generic;
using UnityEngine;

namespace ForeverVillage.Scripts.Character
{
    public class CustomizableCharacter : MonoBehaviour
    {
        [SerializeField] private Transform _headRig;
        [SerializeField] private Transform _handRig;
        [SerializeField] private MeshFilter _head;

        private List<Renderer> _skinRenderers;

        private CharacterHead _characterHead;
        private CharacterBody _characterBody;

        private void Awake()
        {
            _skinRenderers = new List<Renderer>();

            _characterHead = GetComponentInChildren<CharacterHead>();
            _characterHead.Initialize();
            _characterBody = GetComponentInChildren<CharacterBody>();
            _characterBody.Initialize();

            if (_characterHead.IsSkin) _skinRenderers.Add(_characterHead.Renderer);
            if (_characterBody.IsSkin) _skinRenderers.Add(_characterBody.Renderer);
        }

        public Renderer[] SkinRenderers => _skinRenderers.ToArray();
        public Transform HeadRig => _headRig;
        public Transform HandRig => _handRig;
        public MeshFilter Head => _head;
    }
}