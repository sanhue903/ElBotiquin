using System.Collections.Generic;
using UnityEngine;

public class EmotionsAudios : MonoBehaviour
{
    [Header("Audio Elements")]
    [SerializeField] private List<AudioSource> audios;

    public AudioSource GetAudio(int index)
    {
        return audios[index];
    }
}

enum Emotions
{
    Miedo,
    Preocupacion,
    Valentia,
    Felicidad
}