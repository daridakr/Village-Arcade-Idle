using System;

namespace Vampire
{
    public sealed class HealthPoints
    {
        private float _min;
        private float _max;
        private float _current;
        private bool _isClamp;

        public float Current => _current;
        public float Max => _max;

        public event Action Changed;
        public event Action OnEmpty;
        public event Action OnFull;

        public HealthPoints(float minPoints, float maxPoints, bool isClamp = true)
        {
            _min = minPoints;
            _max = maxPoints;
            _current = maxPoints;
            _isClamp = isClamp;
        }

        public void AddPoints(float amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            _current += amount;

            CheckPoints();
            Changed?.Invoke();
        }

        public void SubtractPoints(float amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            _current -= amount;

            CheckPoints();
            Changed?.Invoke();
        }

        public void SetPoints(float amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            _current = amount;

            CheckPoints();
            Changed?.Invoke();
        }

        private void CheckPoints()
        {
            if (_current >= _max)
            {
                OnFull?.Invoke();

                if (_isClamp)
                    _current = _max;
            }
            else if (_current <= _min)
            {
                OnEmpty?.Invoke();

                if (_isClamp)
                    _current = _min;
            }
        }
    }
}