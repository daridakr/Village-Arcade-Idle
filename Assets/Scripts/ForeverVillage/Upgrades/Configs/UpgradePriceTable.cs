using System;
using UnityEngine;

namespace ForeverVillage.Scripts
{
    [Serializable]
    public sealed class UpgradePriceTable
    {
        [Space]
        [SerializeField] private int _basePrice;
        [SerializeField] private int[] _levels;

        public int GetPriceFor(int level)
        {
            var index = level - 1;
            index = Mathf.Clamp(index, 0, _levels.Length - 1);
            return _levels[index];
        }
    }
}