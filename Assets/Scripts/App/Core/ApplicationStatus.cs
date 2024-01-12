using System;
using UnityEngine;

public sealed class ApplicationStatus : MonoBehaviour
{
    public event Action<float> OnUpdate;
    public event Action OnPaused;
    public event Action OnResumed;
    public event Action OnQuit;

    #region Lifecycle

    private void Update()
    {
        OnUpdate?.Invoke(Time.deltaTime);
    }

#if UNITY_EDITOR
    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            OnResumed?.Invoke();
        }
        else
        {
            OnPaused?.Invoke();
        }
    }
#else
        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                OnPaused?.Invoke();
            }
            else
            {
                OnResumed?.Invoke();
            }
        }
#endif

    private void OnApplicationQuit()
    {
        OnQuit?.Invoke();
    }

    #endregion
}
