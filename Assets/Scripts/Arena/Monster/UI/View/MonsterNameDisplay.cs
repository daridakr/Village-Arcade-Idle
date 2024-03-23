using TMPro;
using UnityEngine;

namespace Arena
{
    public class MonsterNameDisplay : CanvasAnimatedView
    {
        [SerializeField] private TMP_Text _nameDisplay;
        [SerializeField] private GameObject _monster;

        private void Start()
        {
            Display();

            _nameDisplay.text = _monster.name;
        }
    }
}