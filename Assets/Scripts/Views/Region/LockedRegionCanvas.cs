using TMPro;
using UnityEngine;

public class LockedRegionCanvas : CanvasAnimatedView
{
    [SerializeField] private TextDisplay _levelView;

    public void Display(int required)
    {
        base.Display();
        _levelView.Display($"Level {required}");
    }
}
