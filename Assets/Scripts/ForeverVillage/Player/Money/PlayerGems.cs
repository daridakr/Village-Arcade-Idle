namespace ForeverVillage
{
    public class PlayerGems : PlayerMoney
    {
        protected override string GetSaveKey()
        {
            return SaveKeyParams.Player.GemsBalance;
        }
    }
}