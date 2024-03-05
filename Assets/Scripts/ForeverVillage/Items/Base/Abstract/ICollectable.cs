using UnityEngine;

namespace ForeverVillage
{
    public interface ICollectable
    {
        public int Value { get; }
        public Collider Collider { get; }
        public void DisableCollision();
    }
}