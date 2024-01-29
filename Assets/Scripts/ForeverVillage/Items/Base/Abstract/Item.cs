using UnityEngine;

namespace Village
{
    [RequireComponent(typeof(Collider))]
    public abstract class Item : MonoBehaviour
    {
        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        public void DisableCollision()
        {
            _collider.enabled = false;
        }
    }
}