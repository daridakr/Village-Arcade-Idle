using UnityEngine;

namespace Vampire
{
    public abstract class IDamageable : MonoBehaviour
    {
        public abstract void TakeDamage(float damage, Vector3 knockback = default);
        public abstract void Knockback(Vector3 knockback);
    }
}
