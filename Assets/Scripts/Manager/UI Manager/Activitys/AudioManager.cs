using UnityEngine;
using RestClient.Core.Singletons;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource backgroundAudio;
    private AudioSource actualAudio;   

    void Start()
    {
        backgroundAudio.Play();
        actualAudio = null;
    }

    public void PlayAudio(AudioSource audio)
    {
        if (actualAudio != null)
        {
            actualAudio.Stop();
        }
        else
            Debug.Log("Audio is null");

        actualAudio = audio;
        actualAudio.Play();
    }

    public void StopAudio()
    {
        if (actualAudio != null)
        {
            actualAudio.Stop();
        }
    }

    public void ChangeBackgroundAudio(AudioSource audio)
    {
        backgroundAudio.Stop();
        backgroundAudio = audio;
        backgroundAudio.Play();
    }
}