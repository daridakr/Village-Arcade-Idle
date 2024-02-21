using IJunior.TypedScenes;
using UnityEngine;
using Village;
using Zenject;

[RequireComponent (typeof(ButtonDisplay))]
public class ArenaLoader : MonoBehaviour
{
    private ButtonDisplay _loadButton;

    private void OnEnable()
    {
        _loadButton = GetComponent<ButtonDisplay>();
        _loadButton.Clicked += OnArenaButtonClicked;
    }

    private void OnArenaButtonClicked()
    {
        IJunior.TypedScenes.Arena.Load();
    }

    private void OnDisable()
    {
        _loadButton.Clicked -= OnArenaButtonClicked;
    }
}
