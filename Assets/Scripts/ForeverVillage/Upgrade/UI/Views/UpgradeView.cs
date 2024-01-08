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
        [SerializeField] private ButtonIconDisplay _upgradeButton;

        private const string MAX = "MAX";

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

        public void SetStats(string current, string next = null)
        {
            _stats.text = $"From <color=#{ColorUtility.ToHtmlStringRGB(Color.red)}>{current}</color>" +
                $" to <color=#{ColorUtility.ToHtmlStringRGB(Color.green)}>{next}</color>";
        }

        public void SetLevel(int level)
        {
            _level.text = $"LV {level}";
        }

        public void SetPrice(int price, int playerBalance)
        {
            _upgradeButton.ChangeTitle(price.ToString());
            _upgradeButton.SetInteractable(playerBalance >= price);
        }

        public void SetMaxLevel()
        {
            _upgradeButton.ChangeTitle(MAX);
            _upgradeButton.SetInteractable(false);
            _upgradeButton.ShowIcon(false);
        }

        private void OnDisable()
        {
            _upgradeButton.Clicked -= OnUpgradeButtonClicked;
        }
    }
}