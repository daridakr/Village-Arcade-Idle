using ForeverVillage;

namespace Village
{
    public sealed class UpgradeBuyer
    {
        private readonly PlayerWallet _wallet;

        public UpgradeBuyer(PlayerWallet wallet)
        {
            _wallet = wallet;
        }

        public bool TryBuy(Upgrade upgrade)
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