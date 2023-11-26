using TMPro;
using UnityEngine;

public class ButtonTextView : ButtonView
{
    [SerializeField] private TMP_Text _name;

    public void ChangeTitle(string newTitle)
    {
        _name.text = newTitle;
    }
}
