using UnityEngine;
using System.Collections.Generic;
using RestClient.Core.Singletons;
using UnityEngine.UI;
public class WhiteboardManager : Singleton<WhiteboardManager>
{
    public Dictionary<string ,AlternativeData> answers;
    public DragBrush brush;

    public Button nextButton;
    
    void Start()
    {
        answers = new Dictionary<string, AlternativeData>();
    }

    public void AddAnswer(string key, AlternativeData answer)
    {
        answers.Add(key, answer);

        nextButton.gameObject.SetActive(true);
    }

    public void NextQuestion()
    {
        foreach (AlternativeData answer in answers.Values)
        {
            ActivityManager.Instance.Answer(answer);
            answer.isCorrect = false;
        }

        answers.Clear();

        brush.ResetBrush();
    }
}