using System.Collections.Generic;
using UnityEngine;

namespace Arena
{
    public sealed class PlayerCharacterModelArena : PlayerCharacterModel,
        ISpatialHashGridClient
    {
        [SerializeField] private Transform _centerTransform; // center of the character, model
        [SerializeField] private Collider meleeHitboxCollider;

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

        public Vector3 Position => transform.position;
        public Vector3 Size => meleeHitboxCollider.bounds.size;
        public Dictionary<int, int> ListIndexByCellIndex { get; set; }
        public int QueryID { get; set; } = -1;
    }
}