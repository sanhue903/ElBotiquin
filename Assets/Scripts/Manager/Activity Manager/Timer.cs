using RestClient.Core.Singletons;
using UnityEngine;

public class Timer : Singleton<Timer>
{
    private float milliseconds = 0;

    void Update()
    {
        milliseconds += Time.deltaTime;
    }   

    public float GetMilliseconds()
    {
        return milliseconds;
    }

    public void ResetTimer()
    {
        milliseconds = 0;
    }
}