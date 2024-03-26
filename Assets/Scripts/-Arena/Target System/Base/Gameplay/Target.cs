using System;
using UnityEngine;

namespace Arena
{
    public abstract class Target : MonoBehaviour,
        ITargetable
    {
        private float _distance;

        public float Distance => _distance;

        public abstract event Action<Target> Inactived;

        public void SetDistanceTo(Vector3 position)
        {
            _distance = Vector3.Distance(transform.position, position);
        }
    }
}