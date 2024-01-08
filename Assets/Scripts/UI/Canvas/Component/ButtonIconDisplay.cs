using UnityEngine;
using UnityEngine.UI;

public class ButtonIconDisplay : ButtonTextDisplay
{
    [SerializeField] private Image _icon;

    public void ShowIcon(bool value)
    {
        _icon.gameObject.SetActive(value);
    }
}
