using ForeverVillage.Scripts;
using IJunior.TypedScenes;
using UnityEngine;

[RequireComponent (typeof(ButtonDisplay))]
public class Match3Loader : MonoBehaviour
{
    [SerializeField] private PlayerCoins _playerCoins;

    private ButtonDisplay _loadButton;

    private void OnEnable()
    {
        _loadButton = GetComponent<ButtonDisplay>();
        _loadButton.Clicked += OnMatch3ButtonClicked;
    }

    private void OnMatch3ButtonClicked()
    {
        Match3Game.Load(_playerCoins);
    }

    private void OnDisable()
    {
        _loadButton.Clicked -= OnMatch3ButtonClicked;
    }
}
