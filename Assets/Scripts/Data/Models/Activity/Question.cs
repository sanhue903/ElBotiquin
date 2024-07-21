using UnityEngine;
using System.Collections.Generic;
public class Question
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