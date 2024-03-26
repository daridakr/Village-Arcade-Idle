using System;
using UnityEngine;

namespace Village
{
    public class BuildingView : MonoBehaviour
    {
        [SerializeField] private ButtonCanvas _upgradeButton;

        private Building _targetToView;
        private UpgradeBuildingPanel _upgradePanel;

        public event Action EarnedNextLevel;

        public void Init(Building target, UpgradeBuildingPanel upgradePanel)
        {
            _targetToView = target;
            _upgradePanel = upgradePanel;
        }

        private void OnEnable()
        {
            _upgradePanel.Upgraded += OnUpgraded;
        }

        public void ShowView()
        {
            _upgradeButton.Display();
            _upgradeButton.ButtonClicked += DisplayUpgradePanel;
        }

        private void DisplayUpgradePanel()
        {
            HideView();

            _upgradePanel.Display(_targetToView.NextLevel);
        }

        private void OnUpgraded()
        {
            _upgradePanel.Hide();

            EarnedNextLevel?.Invoke();
        }

        public void HideView()
        {
            _upgradeButton.Hide();
            _upgradeButton.ButtonClicked -= DisplayUpgradePanel;
        }

        private void OnDisable()
        {
            _upgradePanel.Upgraded -= OnUpgraded;
        }
    }
}