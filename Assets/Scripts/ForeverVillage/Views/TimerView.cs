using UnityEngine;
using UnityEngine.UI;

namespace Village
{
    public class TimerView : CanvasAnimatedView
    {
        [SerializeField] private Image _fillImage;

        private ITimer _timer;
        private float _fullTime;

        private void OnEnable()
        {
            Hide();
        }

        public void Init(ITimer timer)
        {
            //if (_timer != null)
            //    return;

            _timer = timer;

            _timer.Started += OnTimerStart;
            _timer.Updated += OnTimerUpdate;
            //_timer.Stopped += OnTimerStopped;
            _timer.Completed += OnTimerCompleted;
        }

        private void OnTimerStart(float fullTime)
        {
            _fullTime = fullTime;

            Display();
        }

        private void OnTimerUpdate(float ellapsedTime)
        {
            _fillImage.fillAmount = ellapsedTime / _fullTime;
        }

        private void OnTimerCompleted()
        {
            Hide();

            _fillImage.fillAmount = 0f;
        }
    }
}