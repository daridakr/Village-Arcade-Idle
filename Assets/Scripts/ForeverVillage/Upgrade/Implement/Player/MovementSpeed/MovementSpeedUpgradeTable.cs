using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace ForeverVillage.Scripts.Upgrades.Player
{
    [Serializable]
    public sealed class MovementSpeedUpgradeTable
    {
        [SerializeField] private float _startValue;
        [SerializeField] private float _endValue;
        [SerializeField][ReadOnly] private float _step;

        [Space]
        [ReadOnly]
        [ListDrawerSettings(IsReadOnly = true, OnBeginListElementGUI = "DrawLabelForListElement")]
        [SerializeField] private float[] _table;

        public float Step => _step;

        public float GetSpeed(int level) => _table[level - 1];
        public void OnValidate(int maxLevel) => EvaluateTable(maxLevel);

        private void EvaluateTable(int maxLevel)
        {
            _table = new float[maxLevel];
            _table[0] = _startValue;
            _table[maxLevel - 1] = _endValue;

            float step = (_endValue - _startValue) / (maxLevel - 1);
            _step = (float)Math.Round(step, 2);

            for (var i = 1; i < maxLevel - 1; i++)
            {
                var speed = _startValue + _step * i;
                _table[i] = (float)Math.Round(speed, 2);
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