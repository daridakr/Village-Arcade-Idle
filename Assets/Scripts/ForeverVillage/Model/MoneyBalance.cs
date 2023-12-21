using System;
using UnityEngine;

namespace ForeverVillage.Scripts
{
    [Serializable]
    public class MoneyBalance : KeySavedObject<MoneyBalance>
    {
        [SerializeField] private int _value;

        public int Value => _value;

        public event Action ValueChanged;

        public MoneyBalance(string saveKey) : base(saveKey)
        {
        }

        public void Add(int value)
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            _value += value;
            ValueChanged?.Invoke();
        }

        public void Spend(int value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            _value -= value;

            if (_value < 0)
            {
                _value = 0;
            }

            ValueChanged?.Invoke();
        }

        public void Set(int value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            _value = value;
            ValueChanged?.Invoke();
        }

        protected override void OnLoad(MoneyBalance loadedObject)
        {
            _value = loadedObject._value;
        }
    }
}