using System;
using UnityEngine;
using Vampire;

namespace Arena
{
    public abstract class Health : MonoBehaviour,
        IHealth, IDamagable
    {
        private BarPoints _points;

        public event Action<float> Gained;
        public event Action<float> Wasted;
        public event Action Emptied;

        public float ValueNormalazed => _points.Current / _points.Max;

        protected void InitPoints(HealthConfig config)
        {
            _points = new BarPoints(config.Min, config.Max);
            _points.OnEmpty += () => Emptied?.Invoke();
            //_points.Changed += OnHealthChanged;
        }

        //private void OnHealthChanged() => Changed?.Invoke(ValueNormalazed);

        //public void GainHealth(float value)
        //{
        //    CheckForPointsInitialized();

        //    _points.AddPoints(value);
        //    Gained?.Invoke(value);
        //}

        public virtual void TakeDamage(float damage)
        {
            CheckForPointsInitialized();

            _points.SubtractPoints(damage);
            Wasted?.Invoke(damage);
        }

        private void OnDisable()
        {
            _points.OnEmpty -= () => Emptied?.Invoke();
            //_points.Changed -= OnHealthChanged;
        }

        private void CheckForPointsInitialized()
        {
            if (_points == null)
                throw new NullReferenceException(nameof(BarPoints));
        }
    }
}