using System;
using UnityEngine;
using Zenject;

public sealed class GameSaver :
    IInitializable,
    IDisposable
{
    private ApplicationStatus _appStatus;
    private IGameSaveDataListener[] _listeners;
    private float _remainingSeconds;

    private const float SAVE_PERIOD_IN_SECONDS = 30;

    [Inject]
    private void Construct(ApplicationStatus status, IGameSaveDataListener[] listeners)
    {
        _appStatus = status;
        _listeners = listeners;
    }

    private void OnApplicationPaused() => NotifyAboutSave(GameSaveReason.PAUSE);
    private void OnQuitApplication() => NotifyAboutSave(GameSaveReason.QUIT);

    private void OnApplicationUpdate(float deltaTime)
    {
        _remainingSeconds -= deltaTime;

        if (_remainingSeconds <= 0.0f)
        {
            NotifyAboutSave(GameSaveReason.TIMER);
        }
    }

    public void Initialize()
    {
        _remainingSeconds = SAVE_PERIOD_IN_SECONDS;

        _appStatus.OnUpdate += OnApplicationUpdate;
        _appStatus.OnPaused += OnApplicationPaused;
        _appStatus.OnQuit += OnQuitApplication;
    }

    private void NotifyAboutSave(GameSaveReason reason)
    {
        foreach (IGameSaveDataListener listener in _listeners)
        {
            listener.OnSaveData(reason);
        }

        _remainingSeconds = SAVE_PERIOD_IN_SECONDS;
    }

    public void Dispose()
    {
        _appStatus.OnUpdate -= OnApplicationUpdate;
        _appStatus.OnPaused -= OnApplicationPaused;
        _appStatus.OnQuit -= OnQuitApplication;
    }
}
