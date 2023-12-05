using TMPro;
using UnityEngine;

public class ButtonTextDisplay : ButtonDisplay
{
    [SerializeField] private TMP_Text _title;

    public void ChangeTitle(string newTitle)
    {
        _title.text = newTitle;
    }
}
