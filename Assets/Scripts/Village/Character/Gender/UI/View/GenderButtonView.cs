using System;
using UnityEngine;
using UnityEngine.UI;

namespace Village
{
    [RequireComponent(typeof(ButtonDisplay))]
    public sealed class GenderButtonView : MonoBehaviour
    {
        [SerializeField] private Image _icon;

        private ButtonDisplay _button;

        public event Action<GenderButtonView> Selected;

        private void OnEnable()
        {
            _button = GetComponent<ButtonDisplay>();
            _button.Clicked += Select;
        }

        public void SetIcon(Sprite icon)
        {
            _icon.sprite = icon;
        }

        public void SetUnclicked() => _button.SetInteractable(true);
        public void SetClicked() => _button.SetInteractable(false);

        public void Select()
        {
            SetClicked();
            Selected?.Invoke(this);
        }

        private void OnDisable()
        {
            _button.Clicked -= Select;
        }
    }
}