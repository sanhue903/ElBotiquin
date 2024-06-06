using UnityEngine;
using UnityEngine.UI;

public class GetScore : MonoBehaviour
{
    [SerializeField] private string idQuestion; 
    [SerializeField] private bool isCorrect;
    [SerializeField] private string answer;
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
    }

    void SetAnswer(string answer)
    {
        this.answer = answer;
    }
    void OnEnable()
    {
        if (button == null)
        {
            button = GetComponent<Button>();
        }
        
        button.onClick.AddListener(() => {
            Timer timer = Timer.Instance;
            Debug.Log("Time: " + timer.GetMilliseconds());
            ScoreManager.Instance.AddScore(new Score(idQuestion, isCorrect, answer, timer.GetMilliseconds()));
            timer.ResetTimer();
            }
        );
    }
}