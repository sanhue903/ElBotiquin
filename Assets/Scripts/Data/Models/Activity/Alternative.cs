using UnityEngine;
using UnityEngine.UI;

public class Alternative
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
