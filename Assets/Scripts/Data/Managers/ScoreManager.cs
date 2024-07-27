using UnityEngine;
using RestClient.Core.Singletons;
using System.Collections.Generic;
public class ScoreManager : Singleton<ScoreManager>
{
    private List<Score> scores;
    private string chapterId;

    void Start()
    {
        scores = new List<Score>();
    }

    public void SetChapterId(string chapterId)
    {
        this.chapterId = chapterId;
    }

    public void AddScore(Score score)
    {
        scores.Add(score);
        Debug.Log($"Score added:\n {score.GetInfo()}");
    }
//Ver que hacer con los scores que no se envian
    public void SendScores()
    {
        if (scores.Count == 0)
        {
            Debug.Log("No scores to send");
            return;
        }
        
        if (!LoginManager.online)
        {
            Debug.Log("Scores not sent, offline mode");
            return;
        }

        APIManager.Instance.SendScores(chapterId, scores);
    }
}