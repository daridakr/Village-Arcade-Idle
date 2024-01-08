using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ForeverVillage.Scripts
{
    public sealed class UpgradeView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _stats;
        [SerializeField] private TMP_Text _level;
        [SerializeField] private ButtonTextDisplay _upgradeButton;

        public event Action OnUpgradeClick;
        //[SerializeField] private TMP_Text _statsText;

        private void OnEnable()
        {
            _upgradeButton.Clicked += OnUpgradeButtonClicked;
        }

        private void OnUpgradeButtonClicked()
        {
            OnUpgradeClick?.Invoke();
        }

        public void SetIcon(Sprite icon)
        {
            _icon.sprite = icon;
        }

        public void SetTitle(string title)
        {
            _title.text = title;
        }

        public void SetStats(string value, string profit = null)
        {
            string stats = $"Value: {value}";

            if (profit != null)
            {
                stats += $"(+{profit})";
            }

            _stats.text = stats;
        }

        public void SetLevel(int level)
        {
            _level.text = $"LV {level}";
        }

        public void SetPrice(int price)
        {
            _upgradeButton.ChangeTitle(price.ToString());
        }

        private void OnDisable()
        {
            _upgradeButton.Clicked -= OnUpgradeButtonClicked;
        }
    }
}