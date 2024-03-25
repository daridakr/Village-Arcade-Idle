using Zenject;

namespace Arena
{
    public class PlayerTargetDetector : TargetDetector
    {
        private PlayerTarget[] _players;

        [Inject]
        private void Construct([Inject(Id = "Player")] PlayerTarget[] players) => 
            _players = players;

        protected override void Awake()
        {
            base.Awake();

            InitTargets();
        }

        private void InitTargets()
        {
            foreach (PlayerTarget player in _players)
                AddTarget(player);
        }

        // remove target if players quit the game
    }
}