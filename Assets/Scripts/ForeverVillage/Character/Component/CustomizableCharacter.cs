using System.Collections.Generic;
using UnityEngine;

namespace ForeverVillage.Scripts.Character
{
    public class CustomizableCharacter : MonoBehaviour
    {
        private CharacterBody _body;
        private CharacterHead _head;
        private CharacterHair _hair;
        private CharacterBeard _beard;
        private CharacterEyebrows _brows;
        private CharacterEyes _eyes;
        private CharacterMouth _mouth;
        private CharacterHand _hand;

        private List<Renderer> _skinRenderers;
        private List<Renderer> _hairsRenderers;
        private MeshFilter _headMesh;
        private MeshFilter _hairMesh;
        private MeshFilter _beardMesh;
        private MeshFilter _browsMesh;
        private MeshFilter _eyesMesh;
        private MeshFilter _mouthMesh;
        private Transform _headRig;
        private Transform _handRig;

        public Renderer[] SkinRenderers => _skinRenderers.ToArray();
        public Renderer[] HairRenderers => _hairsRenderers.ToArray();
        public MeshFilter HeadMesh => _headMesh;
        public MeshFilter HairMesh => _hairMesh;
        public MeshFilter BeardMesh => _beardMesh;
        public MeshFilter BrowsMesh => _browsMesh;
        public MeshFilter EyesMesh => _eyesMesh;
        public MeshFilter MouthMesh => _mouthMesh;
        public Transform HeadRig => _headRig;
        public Transform HandRig => _handRig;

        private void Awake()
        {
            _skinRenderers = new List<Renderer>();

            InitBody();
            InitHairs();
            InitFace();
            InitRigs();
        }

        private void InitBody()
        {
            _body = GetComponentInChildren<CharacterBody>();
            _body.Initialize();

            _head = GetComponentInChildren<CharacterHead>();
            _head.Initialize();
            _headMesh = _head.Mesh;
            _headRig = _head.Rig;

            if (_head.IsSkin) _skinRenderers.Add(_head.Renderer);
            if (_body.IsSkin) _skinRenderers.Add(_body.Renderer);
        }

        private void InitHairs()
        {
            _hair = GetComponentInChildren<CharacterHair>();
            _hair.Initialize();
            _hairMesh = _hair.Mesh;

            _beard = GetComponentInChildren<CharacterBeard>();
            _beard.Initialize();
            _beardMesh = _beard.Mesh;

            _brows = GetComponentInChildren<CharacterEyebrows>();
            _brows.Initialize();
            _browsMesh = _brows.Mesh;

            _hairsRenderers = new List<Renderer>
            {
                _hair.Renderer,
                _beard.Renderer,
                _brows.Renderer
            };
        }

        private void InitFace()
        {
            _eyes = GetComponentInChildren<CharacterEyes>();
            _eyes.Initialize();
            _eyesMesh = _eyes.Mesh;

            _mouth = GetComponentInChildren<CharacterMouth>();
            _mouth.Initialize();
            _mouthMesh = _mouth.Mesh;
        }

        private void InitRigs()
        {
            _hand = GetComponentInChildren<CharacterHand>();
            _hand.Initialize();
            _handRig = _hand.Rig;
        }
    }
}