using System.Collections;
using System.Collections.Generic;
public class CooldownTimer
{
    public CooldownTimer(float cooldownTime)
    {
        this.cooldownTime = cooldownTime;
        currentTime = cooldownTime;
    }

    public void Update(float time)
    {
        if (isReady)
            return;

        currentTime -= time;
        if (currentTime <= 0)
        {
            currentTime = cooldownTime;
            isReady = true;
        }

    }

    public bool IsReady()
    {
        return isReady;
    }

    public void StartTimer()
    {
        isReady = false;
    }

    public float GetCurrentTime()
    {
        return currentTime;
    }

    private bool isReady = true;
    private float cooldownTime = 0.0f;
    private float currentTime = 0.0f;
}
