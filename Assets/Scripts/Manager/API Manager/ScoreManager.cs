using UnityEngine;
using RestClient.Core.Singletons;
using System.Collections.Generic;
public class ScoreManager : Singleton<ScoreManager>
{
    private List<Score> scores;

    void Start()
    {
        scores = new List<Score>();
    }
    public void AddScore(Score score)
    {
        scores.Add(score);
    }

    public void SendScores()
    {
        APIManager.Instance.SendScores(scores);
    }
}