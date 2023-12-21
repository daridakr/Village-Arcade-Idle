using IJunior.TypedScenes;

namespace ForeverVillage.Scripts
{
    public class PlayerCoins : PlayerMoney, ISceneLoadHandler<int>
    {
        private int _receivedBalace;
        private bool _isReceivedNewBalance;

        // should make separate class like scene enter for game scene, w playercoins link
        public void OnSceneLoaded(int balance)
        {
            _receivedBalace = balance;
            _isReceivedNewBalance = true;
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            if (_isReceivedNewBalance)
            {
                SetNewBalance(_receivedBalace);
            }
        }

        protected override string GetSaveKey()
        {
            return SaveKeyParams.Player.CoinsBalance;
        }
    }
}