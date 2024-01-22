using System;
using UnityEngine;
using UnityEngine.UI;

namespace ForeverVillage.Scripts
{
    [RequireComponent(typeof(ButtonDisplay))]
    public sealed class CustomizationButtonView : MonoBehaviour
    {
        [SerializeField] private Image _icon;

        private ButtonDisplay _button;

        public event Action<CustomizationButtonView> Selected;

        private void OnEnable()
        {
            _button = GetComponent<ButtonDisplay>();
            _button.Clicked += OnClicked;
        }

        public void SetIcon(Sprite icon)
        {
            _icon.sprite = icon;
        }

        public void Select() => _button.SetInteractable(false);
        public void Unselect() => _button.SetInteractable(true);

        private void OnClicked()
        {
            Selected?.Invoke(this);
        }

        private void OnDisable()
        {
            _button.Clicked -= OnClicked;
        }
    }
}