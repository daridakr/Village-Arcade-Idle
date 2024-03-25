using System;
using UnityEngine;

namespace Arena
{
    public abstract class HealthConfig : ScriptableObject
    {
        [SerializeField] private float _minValue = 0f;
        [SerializeField] private float _maxValue = 100f;

        public float Min => _minValue;
        public virtual float Max => _maxValue;

        private void OnValidate()
        {
            try
            {
                Validate();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        protected virtual void Validate()
        {
            if (_minValue < 0) _minValue = 0;
            if (_maxValue <= _minValue) _maxValue = _minValue + 1;
        }
    }
}