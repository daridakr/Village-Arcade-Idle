using TMPro;
using UnityEngine;
using Village;

namespace Vampire.Player
{
    public sealed class NameHudDisplay : CanvasAnimatedView
    {
        [SerializeField] private PlayerName _name;
        [SerializeField] private TMP_Text _nameDisplay;

        private void OnEnable()
        {
            Display();

            _nameDisplay.text = _name.Get();
        }
    }
}