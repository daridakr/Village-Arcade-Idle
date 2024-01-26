using UnityEngine;
using Zenject;

namespace ForeverVillage.Scripts
{
    public sealed class PlayerNameInstaller : MonoBehaviour
    {
        [SerializeField] private GameStarter _starter;

        private PlayerName _playerName;

        [Inject]
        private void Construct(PlayerName name)
        {
            _playerName = name;
        }

        private void Awake()
        {
            if (_starter.IsNewGame) _starter.PlayerNamed += OnPlayerNamed;
            else _playerName.Setup(null);
        }

        private void OnPlayerNamed(string name)
        {
            _starter.PlayerNamed -= OnPlayerNamed;

            _playerName.Setup(name);
        }
    }
}