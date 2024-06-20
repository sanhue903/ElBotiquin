using System;
using UnityEngine;
using UnityEngine.UI;

public class AlternativeData : MonoBehaviour
{
    [SerializeField] private int number;
    [SerializeField] private bool isCorrect;
    [SerializeField] private string answer;
    private Alternative alternative;

    void SetAnswer(string answer)
    {
        this.answer = answer;
    }
    public void SetAlternative(AudioSource audio)
    {
        Button button = GetComponent<Button>();
        alternative = new Alternative(button, audio, isCorrect, answer, number);
        Debug.Log("Alternative: " + alternative.number);
    }

    public Alternative GetAlternative()
    {
        return alternative;
    }
}