using System;
using TMPro;
using UnityEngine;

[RequireComponent (typeof(TMP_InputField))]
public class InputFieldData : MonoBehaviour
{
    [SerializeField] private ButtonView _handlerButton;

    protected TMP_InputField _inputField;

    public event Action<string> DataChanged;
    public event Action<string> DataGetted;

    private void OnEnable()
    {
        _inputField = GetComponent<TMP_InputField>();
        _inputField.onValueChanged.AddListener(OnDataChanged);

        _handlerButton.Clicked += OnButtonClicked;
    }

    private void Start()
    {
        _handlerButton.SetInteractable(!string.IsNullOrEmpty(_inputField.text));
    }

    private void OnDataChanged(string data)
    {
        DataChanged?.Invoke(data);

        if (!string.IsNullOrEmpty(data))
        {
            if (!_handlerButton.Interactable)
            {
                _handlerButton.SetInteractable(true);
            }
        }
        else
        {
            _handlerButton.SetInteractable(false);
        }
    }

    private void OnButtonClicked()
    {
        DataGetted?.Invoke(_inputField.text);
    }

    private void OnDisable()
    {
        _inputField.onValueChanged.RemoveListener(OnDataChanged);
        _handlerButton.Clicked -= OnButtonClicked;
    }
}
