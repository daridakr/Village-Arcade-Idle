using System;

public class Timer : ITimer
{
    private bool _isStarted;

    public float Time { get; private set; }
    public float ElapsedTime { get; private set; }

    // event for UI
    public event Action<float> Started;
    public event Action Stopped;
    public event Action<float> Updated;
    public event Action Completed;

    public void Start(float time)
    {
        ElapsedTime = 0;
        Time = time;
        _isStarted = true;
        Started?.Invoke(Time);
    }

    public void Tick(float tick)
    {
        if (_isStarted == false)
        {
            return;
        }

        ElapsedTime += tick;
        Updated?.Invoke(ElapsedTime);

        if (ElapsedTime >= Time)
        {
            _isStarted = false;
            Completed?.Invoke();
        }
    }

    public void Stop()
    {
        _isStarted = false;
        Stopped?.Invoke();
    }
}

public interface ITimer
{
    public float Time { get; }

    public abstract event Action<float> Started;
    public abstract event Action Stopped;
    public abstract event Action<float> Updated;
    public abstract event Action Completed;
}