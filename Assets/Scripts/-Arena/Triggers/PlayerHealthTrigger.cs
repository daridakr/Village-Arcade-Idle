namespace Arena
{
    public sealed class PlayerHealthTrigger : Trigger<PlayerHealth>
    {
        private PlayerHealth _entered;

        public PlayerHealth Entered => _entered;

        protected override void OnEnter(PlayerHealth triggered) => _entered = triggered;

        protected override void OnExit(PlayerHealth triggered) => _entered = null;
    }
}