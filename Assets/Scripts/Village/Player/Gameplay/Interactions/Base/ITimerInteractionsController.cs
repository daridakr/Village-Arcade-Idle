using System;

namespace Village
{
    public interface ITimerInteractionsController
    {
        public event Action OnInteract;
        public event Action OnFinished;
    }
}