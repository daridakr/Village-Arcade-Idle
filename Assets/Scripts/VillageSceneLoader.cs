using UnityEngine;

[RequireComponent(typeof(ButtonDisplay))]
public sealed class VillageSceneLoader : MonoBehaviour
{
    private ButtonDisplay _loadButton;

    private void OnEnable()
    {
        _loadButton = GetComponent<ButtonDisplay>();
        _loadButton.Clicked += OnVillageButtonClicked;
    }

    private void OnVillageButtonClicked()
    {
        IJunior.TypedScenes.Main.Load();
    }

    private void OnDisable()
    {
        _loadButton.Clicked -= OnVillageButtonClicked;
    }
}
