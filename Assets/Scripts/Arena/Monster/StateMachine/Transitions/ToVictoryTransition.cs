namespace Arena
{
    public class ToVictoryTransition : StateTransition
    {
        private void Update()
        {
            if (_target == null)
                NeedTransit = true;
        }
    }
}