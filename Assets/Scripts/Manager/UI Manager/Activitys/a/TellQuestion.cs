using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TellQuestion : MonoBehaviour
{
    [SerializeField] private Button[] AnswerButtons;
    [SerializeField] private AudioSource QuestionAudio;
    [SerializeField] private AudioSource[] AlternativesAudios;
    [SerializeField] private float offsetTime = 0.5f;
    private float accumulatedSeconds;

    private AudioSource actualAudio;
    
    private float countSeconds;
    private bool isFinished;
    private int ItAudioArray;
    void Start()
    {
        countSeconds = 0;
        ItAudioArray = 0;
        isFinished = false;

        accumulatedSeconds = QuestionAudio.clip.length + offsetTime;
        
        actualAudio = QuestionAudio;
        actualAudio.Play();
    }

    void Update() 
    {
        if (isFinished)
        {
            return;
        }
        
        countSeconds += Time.deltaTime;
        
        if (countSeconds >= accumulatedSeconds)
        {
            if (ItAudioArray < AlternativesAudios.Length)
            {
                if (ItAudioArray > 0)
                    StopAnimation(ItAudioArray - 1);

                actualAudio = AlternativesAudios[ItAudioArray];
                accumulatedSeconds += actualAudio.clip.length + offsetTime;

                actualAudio.Play();
                PlayAnimation(ItAudioArray);

                ItAudioArray++;

                return;
            }

            StopAnimation(ItAudioArray - 1);

            foreach (Button button in AnswerButtons)
            {
                button.interactable = true;
            }
            isFinished = true;
            Timer.Instance.ResetTimer();
        }
    }

    private void PlayAnimation(int It)
    {
        AnswerButtons[It].GetComponent<Animator>().enabled = true;
    } 

    private void StopAnimation(int It)
    {
        AnswerButtons[It].GetComponent<Animator>().SetBool("isLooping", false);
    }
}
