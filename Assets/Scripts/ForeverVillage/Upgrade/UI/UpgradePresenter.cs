namespace Village
{
    public sealed class UpgradePresenter
    {
        private readonly IUpgrade _upgrade;
        private readonly UpgradeView _view;
        private UpgradesController _upgradesController;
        private PlayerWallet _playerWallet;

        public UpgradePresenter(IUpgrade upgrade, UpgradeView view)
        {
            _upgrade = upgrade;
            _view = view;
        }

        public void Initialize(UpgradesController upgradesController, PlayerWallet playerWallet)
        {
            _upgradesController = upgradesController;
            _playerWallet = playerWallet;

            _upgrade.OnLevelUp += OnLevelUped;
            //_moneyStorage.OnMoneyChanged += OnMoneyChanged;

            _view.SetIcon(_upgrade.Icon);
            _view.SetTitle(_upgrade.Title);
            _view.OnUpgradeClick += OnUpgradeClicked;
            UpdateState();
        }

        private void OnLevelUped(int level)
        {
            UpdateState();
        }

        private void OnUpgradeClicked()
        {
            _upgradesController.TryUpgrade(_upgrade);
        }

        private void UpdateState()
        {
            _view.SetLevel(_upgrade.Level + 1);

            if (_upgrade.IsMaxLevel)
            {
                _view.SetStats(_upgrade.CurrentStats);
                _view.SetMaxLevel();
                //set upgrade button state to max
            }
            else
            {
                _view.SetStats(_upgrade.CurrentStats, _upgrade.NextImprovement);
                _view.SetPrice(_upgrade.NextPrice, _playerWallet.Coins);
                // update button state
            }
        }

        public void Dispose()
        {
            _upgrade.OnLevelUp -= OnLevelUped;
            _view.OnUpgradeClick -= OnUpgradeClicked;
        }
    }
}