using ForeverVillage.Scripts;
using IJunior.TypedScenes;
using UnityEngine;
using Zenject;

[RequireComponent (typeof(ButtonDisplay))]
public class ArenaLoader : MonoBehaviour
{
    private PlayerWallet _playerWallet;
    private ButtonDisplay _loadButton;

    [Inject]
    public void Construct(PlayerWallet wallet)
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
        ArenaLobby.Load();
    }

    private void OnDisable()
    {
        _loadButton.Clicked -= OnArenaButtonClicked;
    }
}
