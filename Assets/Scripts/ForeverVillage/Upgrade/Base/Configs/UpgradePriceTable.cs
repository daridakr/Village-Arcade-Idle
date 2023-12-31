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

#if UNITY_EDITOR
        private void DrawLevels(int index)
        {
            GUILayout.Space(8);
            GUILayout.Label($"Level #{index + 1}");
        }

        public void OnValidate(int maxLevel)
        {
            EvaluatePriceTable(maxLevel);
        }

        private void EvaluatePriceTable(int maxLevel)
        {
            var table = new int[maxLevel];
            table[0] = new int();
            for (var level = 2; level <= maxLevel; level++)
            {
                var price = _basePrice * level;
                table[level - 1] = price;
            }

            _levels = table;
        }
#endif
    }
}