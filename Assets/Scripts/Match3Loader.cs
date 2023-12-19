using IJunior.TypedScenes;
using UnityEngine;

[RequireComponent (typeof(ButtonDisplay))]
public class Match3Loader : MonoBehaviour
{
    private ButtonDisplay _loadButton;

    private void OnEnable()
    {
        _loadButton = GetComponent<ButtonDisplay>();

        _loadButton.Clicked += OnMatch3ButtonClicked;
    }

    private void OnMatch3ButtonClicked()
    {
        Match3Game.Load();
    }

    private void OnDisable()
    {
        _loadButton.Clicked -= OnMatch3ButtonClicked;
    }
}
