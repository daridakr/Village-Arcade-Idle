using System;
using UnityEngine;

namespace ForeverVillage.Scripts
{
    [Serializable]
    public class DynamicCost
    {
        [SerializeField] private int _total;
        [SerializeField] private int _current;

        public DynamicCost(int total)
        {
            if (total < 1)
                throw new ArgumentOutOfRangeException(nameof(total));

            _total = total;
            _current = total;
        }

        public int Total => _total;
        public int Current => _current;

        public void Subtract(int value)
        {
            _current -= value;

            if (_current < 0)
                _current = 0;
        }
    }
}