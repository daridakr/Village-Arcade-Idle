using System.Collections.Generic;
using UnityEngine;

namespace Vampire
{
    public sealed class ArenaPlayerCharacterModel : PlayerCharacterModel,
        ISpatialHashGridClient
    {
        [SerializeField] private Transform _centerTransform; // center of the character, model

        private Vector3 _lookDirection = Vector3.right;

        public Transform CenterTransform { get => _centerTransform; }
        public Vector3 LookDirection
        {
            get { return _lookDirection; }
            set
            {
                if (value != Vector3.zero)
                    _lookDirection = value;
            }
        }

        public Vector2 Position => throw new System.NotImplementedException();

        public Vector2 Size => throw new System.NotImplementedException();

        public Dictionary<int, int> ListIndexByCellIndex { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public int QueryID { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    }
}