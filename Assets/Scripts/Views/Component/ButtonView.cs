using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonView : MonoBehaviour
{
    private Button _button;

    public bool Interactable => _button.interactable;

    public event Action Clicked;

    private void OnEnable()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClicked);
    }

    public void SetInteractable(bool value)
    {
        _button.interactable = value;
    }

    private void OnButtonClicked()
    {
        Clicked?.Invoke();
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClicked);
    }
}
