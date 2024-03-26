using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

namespace Arena
{
    [RequireComponent(typeof(Rigidbody))]
    public class KnockbackableEntity : MonoBehaviour,
        IKnockbackable
    {
        protected Rigidbody _rigidbody;

        private bool _canKnockback = false;
        private Vector3 _direction;
        private float _force;
        private float _knockbackTime;

        public event Action OnEnter;
        public event Action OnExit;

        protected virtual void Awake() => _rigidbody = GetComponent<Rigidbody>();

        public void Knockback(float force, Vector3 direction)
        {
            if (force <= 0)
                return;

            OnEnter?.Invoke();

            _direction = direction.normalized;
            _force = force;
            _knockbackTime = CalculateKnockbackTime(_force);
            _canKnockback = true;
        }

        private float CalculateKnockbackTime(float force)
        {
            float speed = force / _rigidbody.mass;
            return speed / Physics.gravity.magnitude;
        }

        protected virtual void FixedUpdate()
        {
            if (!_canKnockback)
                return;

            _rigidbody.AddForce(_direction * _force * Time.fixedDeltaTime, ForceMode.Impulse);
            DelayAsync();
        }

        private async void DelayAsync()
        {
            await UniTask.WaitForSeconds(_knockbackTime);

            OnExit?.Invoke();
            _canKnockback = false;
        }
    }
}