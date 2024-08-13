using UnityEngine;
using UnityEngine.UI;
using RestClient.Core.Singletons;
using System.Collections.Generic;

public class ActivityManager : Singleton<ActivityManager>
{
    [Header("UI Elements")]
    [SerializeField]
    private GameObject introduction;
    [SerializeField]
    private GameObject instructions;
    [SerializeField] private List<GameObject> questions;
    private int questionIndex;
    private bool isStarted;
    private bool hasCalled;

    void Start()
    {
        questionIndex = -1;
        hasCalled = false;
    }

    void Update()
    {
        if (!hasCalled)
        {
            hasCalled = true;
            ActivityUIManager.Instance.StartNoQuestion(introduction);
        }
    }
    public void Answer(AlternativeData answer)
    {
        ActivityDataManager.Instance.Answer(questions[questionIndex].GetComponent<QuestionData>().id, answer);
        ActivityUIManager.Instance.Answer(answer.isCorrect);
    }

    public void NextQuestion()
    {
        if (!isStarted)
        {
            isStarted = true;
            ActivityUIManager.Instance.StartNoQuestion(instructions);
            return;
        }

        questionIndex++;

        if (questionIndex >= questions.Count)
        {
            Debug.Log("No more questions");
            ActivityUIManager.Instance.FinishActivity();
            return;
        }

        ActivityUIManager.Instance.NextQuestion(questions[questionIndex]);
    }

    public QuestionData GetActualQuestionData()
    {
        return questions[questionIndex].GetComponent<QuestionData>();
    }
}