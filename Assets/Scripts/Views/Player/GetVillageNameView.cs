using UnityEngine;

public class GetVillageNameView : CanvasAnimatedView
{
    [SerializeField] private StartGame _start;
    [SerializeField] private InputFieldData _villageNameInput;

    private void OnEnable()
    {
        _start.NewGameStarted += Display;
        _villageNameInput.DataGetted += NameGetted;
    }

    private void NameGetted(string name)
    {
        Hide();
    }

    private void OnDisable()
    {
        _start.NewGameStarted -= Display;
        _villageNameInput.DataGetted -= NameGetted;
    }
}
