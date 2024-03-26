using TMPro;
using UnityEngine;

namespace Arena
{
    public class KilledMonstersDisplay : MonoBehaviour
    {
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private TMP_Text _counterTextDisplay;

        private int _counter = 0;

        private void OnEnable() => _enemySpawner.MonsterKillded += UpdatedDisplay;
        private void Start() => _counterTextDisplay.text = _counter.ToString();
        private void OnDisable() => _enemySpawner.MonsterKillded -= UpdatedDisplay;

        private void UpdatedDisplay() => _counterTextDisplay.text = (++_counter).ToString();
    }
}