using UnityEngine;

namespace Arena
{
    public interface IKnockbackable
    {
        public void Knockback(float force, Vector3 diraction);
    }
}