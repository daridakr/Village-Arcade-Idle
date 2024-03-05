using UnityEngine;

namespace ForeverVillage
{
    [RequireComponent(typeof(Collider))]
    public abstract class Collectable : MonoBehaviour,
        ICollectable
    {
        [SerializeField] private int _value = 1;

        private Collider _collider;

        public int Value => _value;
        public Collider Collider => _collider;

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