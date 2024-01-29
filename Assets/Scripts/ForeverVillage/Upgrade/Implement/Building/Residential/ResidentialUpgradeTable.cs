using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Village.Upgrades.Building
{
    [Serializable]
    public sealed class ResidentialUpgradeTable
    {
        [SerializeField] private GemCapacityUpgradeTable _gemCapacityTable;
        [SerializeField] private GemRateUpgradeTable _gemRateTable;

        public int GetGemCapacity(int level) => _gemCapacityTable.GetValue(level);
        public float GetGemRate(int level) => _gemRateTable.GetValue(level);

        public void OnValidate(int maxLevel)
        {
            _gemCapacityTable.EvaluateTable(maxLevel);
            _gemRateTable.EvaluateTable(maxLevel);
        }

        [Serializable]
        public sealed class GemCapacityUpgradeTable
        {
            [SerializeField] private int _startCapacity;

            [Space]
            [ReadOnly]
            [ListDrawerSettings(IsReadOnly = true, OnBeginListElementGUI = "DrawLabelForListElement")]
            [SerializeField] private int[] _table;

            public int GetValue(int level) => _table[level - 1];

            public void EvaluateTable(int maxLevel)
            {
                _table = new int[maxLevel];
                _table[0] = _startCapacity;

                for (var i = 1; i < maxLevel; i++)
                {
                    _table[i] = _startCapacity * (i + 1);
                }
            }

#if UNITY_EDITOR
            private void DrawLabelForListElement(int index)
            {
                GUILayout.Space(8);
                GUILayout.Label($"Level {index + 1}");
            }
#endif
        }

        [Serializable]
        public sealed class GemRateUpgradeTable
        {
            [SerializeField] private float _baseRate;

            [Space]
            [ReadOnly]
            [ListDrawerSettings(IsReadOnly = true, OnBeginListElementGUI = "DrawLabelForListElement")]
            [SerializeField] private float[] _table;

            public float GetValue(int level) => _table[level - 1];

            public void EvaluateTable(int maxLevel)
            {
                _table = new float[maxLevel];
                _table[0] = _baseRate;

                float rate = _baseRate;

                for (var i = 1; i < maxLevel; i++)
                {
                    rate += _baseRate;
                    _table[i] = rate;
                }
            }

#if UNITY_EDITOR
            private void DrawLabelForListElement(int index)
            {
                GUILayout.Space(8);
                GUILayout.Label($"Level {index + 1}");
            }
#endif
        }
    }
}