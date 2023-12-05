using UnityEngine;

public class MessageDisplayCanvas : ButtonCanvas
{
    [SerializeField] private TextDisplay _textDisplay;

    public void Display(int message, bool interactable = true)
    {
        base.Display();
        _textDisplay.Display(message.ToString());
        _buttonDisplay.SetInteractable(interactable);
    }
}
