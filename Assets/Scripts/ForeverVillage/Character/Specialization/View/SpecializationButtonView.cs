using System;
using UnityEngine;
using UnityEngine.UI;

namespace ForeverVillage.Scripts.Character
{
    [RequireComponent(typeof(ButtonDisplay))]
    public class SpecializationButtonView : MonoBehaviour
    {
        [SerializeField] private Image _icon;

        private ButtonDisplay _button;
        private SpecializationMetadata _info;

        public SpecializationMetadata Info => _info;

        public event Action<SpecializationButtonView> Selected;

        private void OnEnable()
        {
            _button = GetComponent<ButtonDisplay>();
            _button.Clicked += OnSpecClicked;
        }

        public void Initialize(SpecializationMetadata data)
        {
            _info = data;
            Display();
        }

        public void Select() => _button.SetInteractable(false);
        public void Unselect() => _button.SetInteractable(true);

        private void OnSpecClicked()
        {
            Selected?.Invoke(this);
        }

        private void Display()
        {
            _icon.sprite = _info.Icon;
        }

        private void OnDisable()
        {
            _button.Clicked += OnSpecClicked;
        }
    }
}