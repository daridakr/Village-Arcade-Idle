using System;
using UnityEngine;

namespace ForeverVillage.Scripts
{
    public class UpgradeBuildingPanel : CanvasAnimatedView
    {
        [SerializeField] private BuildingLevelView _levelViewPrefab;
        [SerializeField] private PlayerWallet _playerWallet;
        [SerializeField] private RectTransform _content;
        [SerializeField] private ButtonDisplay _destroyButton;

        private BuildingLevelView _currentLevelView;

        public event Action Upgraded;

        public void Display(BuildingLevel nextUpgradeLevel) // , Dictionary<Sprite, int> upgradeStats
        {
            base.Display();

            BuildingLevelView levelView = Instantiate(_levelViewPrefab, _content);
            levelView.Render(nextUpgradeLevel, _playerWallet.Coins, _playerWallet.Gems);
            levelView.Buyed += OnNextLevelBuyed;

            _currentLevelView = levelView;
            _destroyButton.Clicked += OnDestroyButtonClicked;
        }

        private void OnDestroyButtonClicked()
        {
            Debug.Log("lox");
        }

        public override void Hide()
        {
            base.Hide();

            if (_currentLevelView != null)
            {
                _currentLevelView.Buyed -= OnNextLevelBuyed;
                Destroy(_currentLevelView.gameObject);
            }

            _destroyButton.Clicked -= OnDestroyButtonClicked;
        }

        private void OnNextLevelBuyed()
        {
            Upgraded?.Invoke();
        }
    }
}