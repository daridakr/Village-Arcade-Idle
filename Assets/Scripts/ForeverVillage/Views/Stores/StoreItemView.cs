using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ForeverVillage.Scripts
{
    public class StoreItemView<T> : MonoBehaviour where T : IStorableObject
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private ButtonTextDisplay _buyButton;

        private T _buying;

        public event Action<T> Buyed;

        private void OnEnable()
        {
            _buyButton.Clicked += OnBuyButtonClicked;
        }

        public virtual void Render(T buying, int playerBalance)
        {
            _title.text = buying.Name;
            _description.text = buying.Description;
            _image.sprite = buying.Icon;
            _buyButton.ChangeTitle(buying.Price.ToString());
            _buyButton.SetInteractable(playerBalance >= buying.Price);

            _buying = buying;
        }

        private void OnBuyButtonClicked()
        {
            Buyed?.Invoke(_buying);
        }

        private void OnDisable()
        {
            _buyButton.Clicked -= OnBuyButtonClicked;
        }
    }
}