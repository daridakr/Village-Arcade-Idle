using System;
using UnityEngine;
using Vampire;

namespace Arena
{
    public abstract class Health : MonoBehaviour,
        IHealth, IDamagable
    {
        private BarPoints _points;

        public event Action<float> Changed;
        public event Action Emptied;
        public event Action<float> Damaged;

        protected void InitPoints(HealthConfig config)
        {
            _points = new BarPoints(config.Min, config.Max);

            _points.OnEmpty += () => Emptied?.Invoke();
            _points.Changed += (float value) => Changed?.Invoke(value);

            Changed?.Invoke(_points.ValueNormalazed);
        }

        public void GainHealth(float value)
        {
            CheckForInitialized();

            _points.AddPoints(value);
        }

        public virtual void TakeDamage(float damage)
        {
            CheckForInitialized();

            _points.SubtractPoints(damage);
            Damaged?.Invoke(damage);
        }

        private void OnDisable()
        {
            _points.OnEmpty -= () => Emptied?.Invoke();
            _points.Changed -= (float value) => Changed?.Invoke(value);
        }

        private void CheckForInitialized()
        {
            if (_points == null)
                throw new NullReferenceException(nameof(BarPoints));
        }
    }
}