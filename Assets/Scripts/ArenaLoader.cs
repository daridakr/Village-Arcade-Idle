using UnityEngine;
using Village;
using Zenject;

[RequireComponent (typeof(ButtonDisplay))]
public class ArenaLoader : MonoBehaviour
{
    private PlayerWallet _playerWallet;
    private ButtonDisplay _loadButton;

    [Inject]
    private void Construct(PlayerWallet wallet)
    {
        _playerWallet = wallet;
    }

    private void OnEnable()
    {
        _loadButton = GetComponent<ButtonDisplay>();
        _loadButton.Clicked += OnArenaButtonClicked;
    }

    private void OnArenaButtonClicked()
    {
        //ArenaLobby.Load(_playerCoins);
        IJunior.TypedScenes.Arena3D.Load();
    }

    private void OnDisable()
    {
        _loadButton.Clicked -= OnArenaButtonClicked;
    }
}
