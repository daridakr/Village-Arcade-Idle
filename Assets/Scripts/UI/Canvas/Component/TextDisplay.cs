using TMPro;
using UnityEngine;

public class TextDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _display;

    public void Display(string toDisplay)
    {
        _display.text = toDisplay;
    }
}
