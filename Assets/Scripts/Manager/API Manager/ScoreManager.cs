using UnityEngine;
using RestClient.Core.Singletons;
using System.Collections.Generic;
public class ScoreManager : Singleton<ScoreManager>
{
    private List<Score> scores;
    [SerializeField] private string chapterId;

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
        if (LoginManager.online)
        {
            APIManager.Instance.SendScores(chapterId, scores);
        }
    }

    public void ManageScoreMenu(int statusCode)
    {
        switch (statusCode)
        {
            case 201:
                Debug.Log("Scores sent");
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
                break;
            
            case 404: 
                Debug.Log("Scores not sent");
                AuleCodeManager.Instance.ShowCodeMenu();
                break;

            default:
                Debug.Log("Error sending scores");
                break;
        }
    }
}