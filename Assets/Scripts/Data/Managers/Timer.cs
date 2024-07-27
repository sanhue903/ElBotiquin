using RestClient.Core.Singletons;
using UnityEngine;

public class Timer : Singleton<Timer>
{
    private float seconds = 0;

    void Update()
    {
        seconds += Time.deltaTime;
    }   

    public float GetSeconds()
    {
        return seconds;
    }

    public void ResetTimer()
    {
        seconds = 0;
    }
}