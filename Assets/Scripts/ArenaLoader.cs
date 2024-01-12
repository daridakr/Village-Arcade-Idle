using ForeverVillage.Scripts;
using IJunior.TypedScenes;
using UnityEngine;

[RequireComponent (typeof(ButtonDisplay))]
public class ArenaLoader : MonoBehaviour
{
    [SerializeField] private PlayerCoins _playerCoins;

    private ButtonDisplay _loadButton;

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
