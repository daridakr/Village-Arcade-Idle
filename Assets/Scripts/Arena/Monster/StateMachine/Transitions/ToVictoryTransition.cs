namespace Arena
{
    public class ToVictoryTransition : StateTransition
    {
        private void OnEnable()
        {
            foreach (Target target in Targets)
                target.Inactived += (target) => OnWinned(target);
        }

        private void OnWinned(Target target)
        {
            target.Inactived -= OnWinned;
            NeedTransit = true;
        }
    }
}