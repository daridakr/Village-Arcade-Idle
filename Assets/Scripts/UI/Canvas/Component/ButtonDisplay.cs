using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonDisplay : MonoBehaviour
{
    private Button _button;

    public bool Interactable => _button.interactable;

    public event Action Clicked;
    public event Action<ButtonDisplay> Selected;

    private void OnEnable()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClicked);
    }

    public void SetInteractable(bool value)
    {
        _button.interactable = value;
    }

    public void OnButtonClicked()
    {
        Clicked?.Invoke();
        Selected?.Invoke(this);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClicked);
    }
}
