using UnityEngine;
using UnityEngine.UI;
using RestClient.Core.Singletons;
using System.Collections.Generic;
using System;

public class ActivityManager : Singleton<ActivityManager>
{
    [Header("UI Elements")]
    [SerializeField] private GameObject questionsParent;
    [SerializeField] private Button nextButton;

    [Header("Audio Settings")]
    [SerializeField] private float offsetTime = 0.5f;

    [Header("Information")]
    [SerializeField] private string chapterId;
    
    private List<QuestionData> questions;

    // time management
    private float audiosTotalSeconds;
    private float currentSeconds;
    private bool isStarted;

    // UI management
    private bool audioIsPlaying;
    private int questionIndex;
    private Question actualQuestion;
    private int alternativeIndex;
    private Alternative actualAlternative;
    
    void Start()
    {
        isStarted = false;
        currentSeconds = 0;
        audiosTotalSeconds = 0;
        audioIsPlaying = false;
        alternativeIndex = 0;
        questionIndex = 0;
        
        questions = new List<QuestionData>();       
    } 

    public void NextQuestion()
    {
        if (!isStarted)
        {
            isStarted = true;
            InitActivity();
            return;
        }

        if (questionIndex >= questions.Count)
        {
            ScoreManager.Instance.SendScores();
            SceneManager.Instance.LoadScene("MainMenu");
            return;
        }

        questions[questionIndex].gameObject.SetActive(false);
        questionIndex++;
        StartQuestion(questionIndex);
    }

    public void InitActivity()
    {
        int childCount = questionsParent.transform.childCount;
        for (int i = 1; i < childCount; i++)
        {
            QuestionData questionData = questionsParent.transform.GetChild(i).GetComponent<QuestionData>();

            questions.Add(questionData);
        }

        ScoreManager.Instance.SetChapterId(chapterId);
        questionsParent.transform.GetChild(0).gameObject.SetActive(false);

        Debug.Log("Activity Started");
        StartQuestion(0);
    }  

    public void StartQuestion(int index)
    {
        if (questions.Count > index)
        {
            ResetValues();
            
            questions[index].gameObject.SetActive(true);
            actualQuestion = questions[index].GetQuestion();
            
            Debug.Log("Starting Question: " + actualQuestion.number);

            AudioManager.Instance.PlayAudio(actualQuestion.question);
            
            audiosTotalSeconds = actualQuestion.question.clip.length + offsetTime;
            audioIsPlaying = true;
        }
    }
    void Update()
    {
        if (audioIsPlaying)
        {
            UpdateQuestion();
        }
    }

    private void UpdateQuestion()
    {
        currentSeconds += Time.deltaTime;

        if (currentSeconds < audiosTotalSeconds)
        {
            return;
        }

        if (alternativeIndex >= actualQuestion.alternatives.Count)
        {
            AlternativeManager.Instance.StopAnimation(actualAlternative);
            EnableAllAlternatives();    
            audioIsPlaying = false;
            return;
        }
        
        if (alternativeIndex > 0)
        {
            AlternativeManager.Instance.StopAnimation(actualAlternative);
        }

        actualAlternative = actualQuestion.alternatives[alternativeIndex];
        
        AudioManager.Instance.PlayAudio(actualAlternative.audio);
        AlternativeManager.Instance.TellAlternative(actualAlternative);

        audiosTotalSeconds += actualAlternative.audio.clip.length + offsetTime;
        alternativeIndex++;
    }

    private void EnableAllAlternatives()
    {
        foreach (Alternative alternative in actualQuestion.alternatives)
        {
            AlternativeManager.Instance.EnableAlternaitve(alternative);
        }
    }

    private void DisableAllAlternatives()
    {
        foreach (Alternative alternative in actualQuestion.alternatives)
        {
            AlternativeManager.Instance.DisableAlternaitve(alternative);
        }
    }

    private void ResetValues()
    {
        currentSeconds = 0;
        audiosTotalSeconds = 0;
        alternativeIndex = 0;
    }

    public void Response(int index)
    {
        index--;
        if (index  >= actualQuestion.alternatives.Count)
        {
            Debug.LogError("Index out of range");
            return;
        }

        Alternative alternative = actualQuestion.alternatives[index];
        SendScore(alternative);

        if (!alternative.isCorrect)
        {
            AudioManager.Instance.PlayAudio(actualQuestion.incorrectAnswer);
            return;
        }

        AudioManager.Instance.PlayAudio(actualQuestion.correctAnswer);
        DisableAllAlternatives();
        nextButton.gameObject.SetActive(true);
        actualQuestion.winningScreen.SetActive(true);
    }

    public void SendScore(Alternative alternative)
    {
        Timer timer = Timer.Instance;
        float time = timer.GetMilliseconds();

        Debug.Log("Time: " + time);
        ScoreManager.Instance.AddScore(new Score(actualQuestion.questionId, alternative.isCorrect, alternative.answer, time));
        timer.ResetTimer();
    }
} 

public struct Alternative
{
    //UI Elements
    public Button button;

    //Audio Elements
    public AudioSource audio;

    //Information
    public bool isCorrect;
    public string answer;
    public int number;

    public Alternative(Button button, AudioSource audio, bool isCorrect, string answer, int number)
    {
        this.button = button;
        this.audio = audio;
        this.isCorrect = isCorrect;
        this.answer = answer;
        this.number = number;
    }
}
public struct Question
{
    //UI Elements
    public List<Alternative> alternatives; 
    public GameObject winningScreen;

    //Audio Elements
    public AudioSource correctAnswer;
    public AudioSource incorrectAnswer;
    public AudioSource question;

    //Information
    public string questionId;
    public int number;

    public Question(List<Alternative> alternatives, GameObject winningScreen, AudioSource correctAnswer, AudioSource incorrectAnswer, AudioSource question, string questionId, int number)
    {
        this.alternatives = alternatives;
        this.winningScreen = winningScreen;
        this.correctAnswer = correctAnswer;
        this.incorrectAnswer = incorrectAnswer;
        this.question = question;
        this.questionId = questionId;
        this.number = number;
    }
}