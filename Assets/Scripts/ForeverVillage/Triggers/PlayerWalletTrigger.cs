namespace Village
{
    public class PlayerWalletTrigger : Trigger<PlayerWallet>
    {
        private PlayerWallet _entered;

        public PlayerWallet Entered => _entered;

        protected override void OnEnter(PlayerWallet triggered)
        {
            _entered = triggered;
        }

        protected override void OnExit(PlayerWallet triggered)
        {
            _entered = null;
        }
    }
}