using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class ButtonView : MonoBehaviour
{
    private Button _button;

    public event Action Clicked;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
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
