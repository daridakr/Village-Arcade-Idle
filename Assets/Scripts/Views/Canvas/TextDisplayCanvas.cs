using UnityEngine;

public class TextDisplayCanvas : CanvasAnimatedView
{
    [SerializeField] private TextDisplay _textDisplay;

    public void Display(string value)
    {
        base.Display();
        _textDisplay.Display(value);
    }
}
