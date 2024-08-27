using UnityEngine;
using UnityEngine.UI;
using RestClient.Core.Singletons;
using System.Collections.Generic;

public class ActivityUIManager : Singleton<ActivityUIManager>
{
    [Header("UI Elements")]
    [SerializeField] private Button nextButton;
    [SerializeField] private Button instructionsButton;
    [Header("Audio Settings")]
    [SerializeField] private float offSetTime = 0.5f;
    [Header("Scene")]
    [SerializeField] private string nextScene;
    
    //Management
    private float audioLength;
    private bool isReading;
    private bool isIntroduction;

    private int alternativeIndex;

    private GameObject actualQuestion;
    void Start()
    {
        isReading = false;
        isIntroduction = true;
    }

    void Update()
    {
        if (isReading)
        {
            if (Timer.Instance.GetSeconds() < audioLength)
            {
                return;
            }

            if (isIntroduction)
            {
                FinishNoQuestion();
                Debug.Log("Finished intro");
                return;
            }

            alternativeIndex++;

            bool isFinished = !StartAlternative(alternativeIndex);

            if (isFinished)
            {
                FinishQuestion(actualQuestion);
            }
        }
    }
    
    public void StartNoQuestion(GameObject noQuestion)
    {   
        if (actualQuestion != null)
        {
            actualQuestion.SetActive(false);
        }
        actualQuestion = noQuestion;
        noQuestion.gameObject.SetActive(true);

        instructionsButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);

        ReadNoQuestion(noQuestion);
    }
    public void ReadNoQuestion(GameObject noQuestion)
    {
        Debug.Log($"Reading intro");

        AudioSource audio = noQuestion.GetComponent<AudioSource>();

        if (audio.clip == null)
        {
            Debug.LogError("No audio clip found");
        }
        audioLength = audio.clip != null ? audio.clip.length + offSetTime : 1;
        
        Timer.Instance.ResetTimer();
        isReading = true;

        AudioManager.Instance.PlayAudio(audio);
    }

    public void FinishNoQuestion()
    {
        isReading = false;
        nextButton.gameObject.SetActive(true);
    }

    private void StartQuestion(GameObject question)
    {
        isIntroduction = false;
        instructionsButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);
        
        alternativeIndex = -1;

        question.SetActive(true);
        actualQuestion = question;
        ReadQuestion(question.GetComponent<QuestionUI>());
    }

    private void ReadQuestion(QuestionUI question)
    {
        Debug.Log($"Reading question: {question.name}");

        audioLength = question.GetQuestionAudioLength() + offSetTime;
        Timer.Instance.ResetTimer();
        isReading = true;

        AudioManager.Instance.PlayAudio(question.questionAudio);
    }

    private void FinishQuestion(GameObject question)
    {
        isReading = false;
        Timer.Instance.ResetTimer();
        instructionsButton.gameObject.SetActive(true);

        List<AlternativeUI> alternatives = question.GetComponent<QuestionUI>().alternatives;
        foreach (AlternativeUI alternative in alternatives)
        {
            alternative.EnableAlternative();
        }
    }
    private bool StartAlternative(int index)
    {
        List<AlternativeData> alternatives = actualQuestion.GetComponent<QuestionData>().alternatives;

        if (index > 0)
        {
            GameObject previousAlternative = alternatives[index - 1].gameObject;
            previousAlternative.GetComponent<AlternativeUI>().StopAnimation();
        }

        if (index >= alternatives.Count)
        {
            Debug.Log("No more alternatives");
            return false;
        }

        GameObject actualAlternative = alternatives[index].gameObject;
        ReadAlternative(actualAlternative.GetComponent<AlternativeUI>());

        return true;
    }

    private void ReadAlternative(AlternativeUI alternative)
    {
        Debug.Log($"Reading alternative: {alternative.name}");

        audioLength = alternative.GetAudioLength() + offSetTime;
        Timer.Instance.ResetTimer();
        isReading = true;

        alternative.ReadAlternative();
    }

    public void NextQuestion(GameObject nextQuestion)
    {
        if (actualQuestion != null)
        {
            actualQuestion.SetActive(false);
        }
        StartQuestion(nextQuestion);
    }

    public void FinishActivity()
    {
        Debug.Log("Finishing activity");
        
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
    }
    public void Answer(bool isCorrect)
    {
        if (isCorrect)
        {
            Debug.Log("Correct answer");
            AudioManager.Instance.PlayAudio(actualQuestion.GetComponent<QuestionUI>().correctAnswerAudio);

            nextButton.gameObject.SetActive(true);

            GameObject winningScreen = actualQuestion.GetComponent<QuestionUI>().winningScreen;

            if (winningScreen != null)
            {
                winningScreen.SetActive(true);
            }
        }
        else
        {
            Debug.Log("Incorrect answer");
            AudioManager.Instance.PlayAudio(actualQuestion.GetComponent<QuestionUI>().incorrectAnswerAudio);
        }   
    }
}