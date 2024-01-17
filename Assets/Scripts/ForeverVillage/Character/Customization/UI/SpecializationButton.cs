using System;
using UnityEngine;
using UnityEngine.UI;

namespace ForeverVillage.Scripts.Character
{
    [RequireComponent(typeof(ButtonDisplay))]
    public class SpecializationButton : MonoBehaviour
    {
        [SerializeField] private Image _icon;

        private ButtonDisplay _button;

        public event Action<SpecializationButton> Selected;

        private void OnEnable()
        {
            _button = GetComponent<ButtonDisplay>();
            _button.Clicked += OnSpecClicked;
        }

        public void Initialize(SpecializationConfig config)
        {
            _icon.sprite = config.Icon;

        }

        private void OnSpecClicked()
        {
            _button.SetInteractable(false);

            Selected?.Invoke(this);
        }

        private void OnDisable()
        {
            _button.Clicked += OnSpecClicked;
        }
    }
}