using ForeverVillage;

namespace Arena
{
    public sealed class CoinsDisplayArena : PlayerMoneyDisplay<PlayerCoins>
    {
        private int _amount = 0;

        protected override string SetStartedValue() => _amount.ToString();
        protected override string SetChangedValue(int value) => (_amount += value).ToString();
    }
}