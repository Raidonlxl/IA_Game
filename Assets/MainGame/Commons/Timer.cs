using UnityEngine;

public class Timer 
{
    public float currentTime;

    public float maxTime;

    public Timer (float currentTime, float maxTime)
    {
        this.currentTime = currentTime;
        this.maxTime = maxTime;
    }

    public bool IsCompleted()
    {
        if (currentTime < maxTime)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void Run()
    {
        if (currentTime < maxTime)
        {
            currentTime += Time.deltaTime;
        }
    }

    public void ResetTimer()
    {
        currentTime = 0;
    }
}
