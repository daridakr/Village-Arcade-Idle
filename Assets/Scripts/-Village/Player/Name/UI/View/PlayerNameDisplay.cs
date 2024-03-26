using TMPro;
using UnityEngine;
using Zenject;

namespace Village
{
    public sealed class PlayerNameDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nameDisplay;

        private PlayerName _playerName;

        [Inject]
        private void Construct(PlayerName playerName) => _playerName = playerName;

        private void Display(string name) => _nameDisplay.text = name;

        private void OnEnable() => _playerName.Setuped += Display;
        private void OnDisable() => _playerName.Setuped -= Display;
    }
}