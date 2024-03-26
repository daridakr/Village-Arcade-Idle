using ForeverVillage;

namespace Village
{
    public sealed class CoinsDisplay : PlayerMoneyDisplay<PlayerCoins>
    {
        protected override string SetStartedValue() => Current;
        protected override string SetChangedValue(int value) => Current;
    }
}