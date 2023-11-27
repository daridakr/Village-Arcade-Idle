using System;
using UnityEngine;

public abstract class BuildingInteraction : MonoBehaviour
{
    [SerializeField] private PlayerAnimation _playerAnimation;
    [SerializeField] private TimerView _timerView;

    private readonly Timer _timer = new Timer();
    private float _interactionTime = 3f;
    private string _animationParametr;

    public event Action Started;
    public event Action Stopped;

    private void OnEnable()
    {
        _timerView.Init(_timer);
    }

    private void Update()
    {
        _timer.Tick(Time.deltaTime);
    }

    protected void StartInteract(BuildingZone zone, string animParam)
    {
        _animationParametr = animParam;
        _playerAnimation.StartInteract(_animationParametr);

        _timer.Start(_interactionTime);
        _timer.Completed += OnCompleted;

        Started?.Invoke();
    }

    protected virtual void OnCompleted()
    {
        _playerAnimation.StopInteract(_animationParametr);
        _timer.Completed -= OnCompleted;
        Stopped?.Invoke();
    }
}
