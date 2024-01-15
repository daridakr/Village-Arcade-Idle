namespace ForeverVillage.Scripts
{
    public sealed class MoneyUpgrader
    {
        private readonly PlayerWallet _wallet;

        public MoneyUpgrader(PlayerWallet wallet)
        {
            _wallet = wallet;
        }

        public PlayerWallet Wallet => throw new System.NotImplementedException();

        public bool TryUpgrade(Upgrade upgrade)
        {
            if (upgrade.IsMaxLevel || _wallet.IsEmpty)
            {
                return false;
            }

            var price = upgrade.NextPrice;

            if (price > _wallet.Coins)
            {
                return false;
            }

            _wallet.SpendCoins(price);
            upgrade.IncrementLevel();

            return true;
        }
    }
}