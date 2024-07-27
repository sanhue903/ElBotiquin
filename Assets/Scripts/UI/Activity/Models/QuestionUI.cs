using UnityEngine;
using System.Collections.Generic;

public class QuestionUI : MonoBehaviour
{
    [Header("Audio Elements")]
    public AudioSource questionAudio;
    public AudioSource correctAnswerAudio;
    public AudioSource incorrectAnswerAudio;
    public List<AlternativeUI> alternatives;
    [Header("Visual Elements")]
    public GameObject winningScreen;
    void Start()
    {
        foreach (var alternative in GetComponent<QuestionData>().alternatives)
        {
            if (alternative.TryGetComponent<AlternativeUI>(out var aui))
            {
                alternatives.Add(aui);
            }
        }
    }

    public float GetQuestionAudioLength()
    {
        return questionAudio.clip.length;
    }

    public AlternativeUI GetAlternative(int index)
    {
        return alternatives[index];
    }
}