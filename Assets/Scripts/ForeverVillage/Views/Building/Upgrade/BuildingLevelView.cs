using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ForeverVillage.Scripts
{
    public class BuildingLevelView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private Image _image;
        [SerializeField] private ButtonTextDisplay _buyButton;

        public event Action Buyed;

        private void OnEnable()
        {
            _buyButton.Clicked += OnBuyButtonClicked;
        }

        public virtual void Render(BuildingLevel level, int playerCoins, int playerGems)
        {
            _title.text = level.Name;
            _image.sprite = level.Icon;
            _buyButton.ChangeTitle(SetPrice(level.Price, level.GemsPrice));
            _buyButton.SetInteractable(playerCoins >= level.Price && playerGems >= level.GemsPrice);
        }

        private string SetPrice(int coins, int gems)
        {
            string coinsPrice = coins != 0 ? coins.ToString() : string.Empty;
            string gemsPrice = gems != 0 ? gems.ToString() : string.Empty;

            return coinsPrice + gemsPrice;
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
