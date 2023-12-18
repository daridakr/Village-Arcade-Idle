namespace ForeverVillage.Scripts
{
    public class PlayerCoinsTrigger : Trigger<PlayerCoins>
    {
        private PlayerCoins _entered;

        public PlayerCoins Entered => _entered;

        protected override void OnEnter(PlayerCoins triggered)
        {
            _entered = triggered;
        }

        protected override void OnExit(PlayerCoins triggered)
        {
            _entered = null;
        }
    }
}