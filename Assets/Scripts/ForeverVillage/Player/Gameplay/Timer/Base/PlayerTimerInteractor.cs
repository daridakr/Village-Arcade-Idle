using System;
using UnityEngine;

namespace ForeverVillage.Scripts
{
    public abstract class PlayerTimerInteractor : MonoBehaviour
    {
        [SerializeField] private TimerView _timerView;
        [SerializeField] private GameObject _interactorItem;

        private PlayerAnimation _playerAnimation;
        private Transform _rigForItem;
        private GameObject _interactorItemInstance;

        private readonly Timer _timer = new Timer();
        private const float _interactionTime = 3f;
        private string _animationParametr;

        public event Action<PlayerTimerInteractor> Started;
        public event Action Stopped;

        private void OnEnable()
        {
            _timerView.Init(_timer);
        }

        public void Setup(PlayerAnimation animation, Transform rigForItem = null)
        {
            _playerAnimation = animation;

            if (rigForItem == null)
                _rigForItem = transform;
            else
                _rigForItem = rigForItem;
        }

        private void Update()
        {
            _timer.Tick(Time.deltaTime);
        }

        protected void StartInteract(string animParam)
        {
            _animationParametr = animParam;
            _playerAnimation.StartInteract(_animationParametr);

            _timer.Start(_interactionTime);
            _timer.Completed += OnCompleted;

            if (_interactorItem != null)
                _interactorItemInstance = Instantiate(_interactorItem, _rigForItem.transform);

            Started?.Invoke(this);
        }

        protected virtual void OnCompleted()
        {
            _playerAnimation.StopInteract(_animationParametr);
            _timer.Completed -= OnCompleted;

            if (_interactorItemInstance != null)
                Destroy(_interactorItemInstance.gameObject);

            Stopped?.Invoke();
        }
    }
}