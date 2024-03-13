using UnityEngine;

namespace Arena
{
    public interface IKnockbackable
    {
        public void Knockback(Vector3 force);
    }
}