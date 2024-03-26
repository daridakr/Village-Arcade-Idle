using ForeverVillage;

namespace Village
{
    public sealed class GemsDisplay : PlayerMoneyDisplay<PlayerGems>
    {
        protected override string SetStartedValue() => Current;
        protected override string SetChangedValue(int value) => Current;
    }
}