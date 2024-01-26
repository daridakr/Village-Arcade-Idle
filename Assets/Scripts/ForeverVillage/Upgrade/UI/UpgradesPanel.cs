using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ForeverVillage.Scripts
{
    [AddComponentMenu("Upgrades/Upgrades Panel")]
    public class UpgradesPanel : CanvasAnimatedView
    {
        [SerializeField] private UpgradesController _upgradesController;
        [SerializeField] private UpgradeView _viewPrefab;
        [SerializeField] private Transform _content;

        private List<UpgradePresenter> _presenters;
        private List<UpgradeView> _views;
        private PlayerWallet _playerWallet;

        [Inject]
        private void Construct(PlayerWallet playerWallet)
        {
            _playerWallet = playerWallet;
        }

        public UpgradesPanel()
        {
            _presenters = new List<UpgradePresenter>();
            _views = new List<UpgradeView>();
        }

        public override void Display()
        {
            base.Display();
            CreateUpgrades();
        }

        public override void Hide()
        {
            base.Hide();
            ClearUpgrades();
        }

        private void CreateUpgrades()
        {
            IUpgrade[] upgrades = _upgradesController.GetAllUpgrades();

            foreach (var model in upgrades)
            {
                UpgradeView view = Instantiate(_viewPrefab, _content);
                _views.Add(view);

                var presenter = new UpgradePresenter(model, view);
                presenter.Initialize(_upgradesController, _playerWallet);
                _presenters.Add(presenter);
            }
        }

        private void ClearUpgrades()
        {
            foreach (var presenter in _presenters)
            {
                presenter.Dispose();
            }

            _presenters.Clear();

            foreach (var view in _views)
            {
                Destroy(view.gameObject);
            }

            _views.Clear();
        }
    }
}