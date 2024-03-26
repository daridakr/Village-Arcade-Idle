using System;
using UnityEngine;

namespace Village
{
    public abstract class TimerInteraction : MonoBehaviour,
        IInteraction
    {
        [SerializeField] private TimerView _timerView;
        [SerializeField] private GameObject _interactorItem;

        private Transform _rigForItem;
        private GameObject _interactorItemInstance;

        private readonly Timer _timer = new Timer();
        private const float _interactionTime = 3f;

        public event Action Started;
        public event Action Stopped;

        protected virtual void OnEnable()
        {
            _timerView.Init(_timer);
        }

        public void Setup(Transform rigForItem = null)
        {
            if (rigForItem == null)
                _rigForItem = transform;
            else
                _rigForItem = rigForItem;
        }

        private void Update()
        {
            _timer.Tick(Time.deltaTime);
        }

        protected virtual void StartInteract()
        {
            _timer.Start(_interactionTime);
            _timer.Completed += OnCompleted;

            if (_interactorItem != null)
                _interactorItemInstance = Instantiate(_interactorItem, _rigForItem.transform);

            Started?.Invoke();
        }

        protected virtual void OnCompleted()
        {
            _timer.Completed -= OnCompleted;

            if (_interactorItemInstance != null)
                Destroy(_interactorItemInstance.gameObject);

            Stopped?.Invoke();
        }
    }
}