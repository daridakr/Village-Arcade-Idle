using Arena;
using UnityEngine;

namespace LlamAcademy.Guns.Demo
{
    [RequireComponent(typeof(Collider))]
    public class KnockbackOnCollision : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.TryGetComponent(out IKnockbackable knockbackable))
            {
                //knockbackable.Knockback(-other.impulse / Time.fixedDeltaTime);
            }
        }
    }
}