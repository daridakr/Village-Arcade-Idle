using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Village
{
    public class BuildingLevelView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _level;
        [SerializeField] private HorizontalLayoutGroup _statsGroup;
        [SerializeField] private BuildingStatView _statTemplate;
        [SerializeField] private ButtonTextDisplay _buyButton;
        [SerializeField] private PriceDisplay _coinsPrice;
        [SerializeField] private PriceDisplay _gemsPrice;

        public event Action Buyed;

        private void OnEnable()
        {
            _buyButton.Clicked += OnBuyButtonClicked;
        }

        public virtual void Render(BuildingLevel level, int playerCoins, int playerGems)
        {
            _title.text = level.Name;
            _image.sprite = level.Icon;
            _level.text = $"LV {level.Value}";
            _coinsPrice.SetPrice(level.Price);
            _gemsPrice.SetPrice(level.GemsPrice);
            _buyButton.SetInteractable(playerCoins >= level.Price && playerGems >= level.GemsPrice);
        }

        private void OnBuyButtonClicked()
        {
            Buyed?.Invoke();
        }

        private void OnDisable()
        {
            _buyButton.Clicked -= OnBuyButtonClicked;
        }
    }
}
