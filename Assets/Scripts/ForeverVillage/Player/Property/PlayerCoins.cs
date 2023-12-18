namespace ForeverVillage.Scripts
{
    public class PlayerCoins : PlayerMoney
    {
        protected override string GetSaveKey()
        {
            return SaveKeyParams.Player.CoinsBalance;
        }
    }
}