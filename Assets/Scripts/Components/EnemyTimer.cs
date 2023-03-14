using System;
using UnityEngine;

public class EnemyTimer : MonoBehaviour
{
    public float Duration { get; set; }

    public bool IsRunning { get; private set; }

    public event EventHandler OnTimerElapsed;

    private float elapsedTime;

    public void Run()
    {
        IsRunning = true;
    }

    public void Pause()
    {
        IsRunning = false;
    }

    public void Reset()
    {
        elapsedTime = 0f;
        IsRunning = false;
    }

    private void Update()
    {
        if (IsRunning)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= Duration)
            {
                IsRunning = false;
                OnTimerElapsed?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
