using UnityEngine;
using RestClient.Core.Singletons;

public class ActivityDataManager : Singleton<ActivityDataManager>
{
    [SerializeField]
    private string chapterId;
    void Start()
    {
        ScoreManager.Instance.SetChapterId(chapterId);
    }
    public void Answer(string questionId, AlternativeData answer)
    {
        Score score = new Score(questionId, answer.isCorrect, answer.answer, Timer.Instance.GetSeconds());
        ScoreManager.Instance.AddScore(score);
        Timer.Instance.ResetTimer();
    }

    public void SendAlternatives()
    {
        ScoreManager.Instance.SendScores();
    }
}
