using System;
using UnityEngine;

public class BuildingCleaner : MonoBehaviour
{
    [SerializeField] private TimerView _timerView;
    [SerializeField] private PlayerAnimation _playerAnimation;

    private readonly Timer _timer = new Timer();
    private float _interactionTime = 3f; // should define new base entity for interaction, smth like that

    public event Action StartedCleaning; // with this event fields like 'Start' and 'Stop' 
    public event Action StoppedCleaning;

    private void OnEnable()
    {
        _timerView.Init(_timer);
    }

    private void Update()
    {
        _timer.Tick(Time.deltaTime);
    }

    public void StartClean(BuildingZone zone)
    {
        if (zone.State == BuildingZoneState.Destroyed)
        {
            _playerAnimation.StartCleaning();
            _timer.Start(_interactionTime);
            _timer.Completed += OnCleanCompleted;

            StartedCleaning?.Invoke();
        }

        return;
    }

    private void OnCleanCompleted()
    {
        _timer.Completed -= OnCleanCompleted;
        _playerAnimation.StopCleaning();

        StoppedCleaning?.Invoke();
    }

    private void OnDisable()
    {
        
    }
}
