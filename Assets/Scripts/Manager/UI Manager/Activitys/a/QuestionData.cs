using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestionData : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject[] AlternativesParents;
    [SerializeField] private GameObject winningScreen;

    [Header("Audio Elements")]
    [SerializeField] private GameObject[] AudiosParents;
    [SerializeField] private AudioSource question;
    [SerializeField] private AudioSource correctAnswer;
    [SerializeField] private AudioSource incorrectAnswer;

    [Header("Information")]
    [SerializeField] private string questionId; 
    [SerializeField] private int number;
    
    private Question ThisQuestion;
    private List<Alternative> alternatives;
    void Awake()
    {
        alternatives = new List<Alternative>();

        for (int i = 0; i < AlternativesParents.Length; i++)
        {
            AddAlternatives(AudiosParents[i], AlternativesParents[i]);
        }

        ThisQuestion = new Question(alternatives, winningScreen, correctAnswer, incorrectAnswer, question, questionId, number);
        Debug.Log("Question: " + ThisQuestion.number);
    }

    void AddAlternatives(GameObject audiosParent, GameObject alternativesParent)
    {
        int childCount = alternativesParent.transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            AlternativeData alternativeData = alternativesParent.transform.GetChild(i).GetComponent<AlternativeData>();
            alternativeData.SetAlternative(audiosParent.transform.GetChild(i).GetComponent<AudioSource>());


            Alternative alternative = alternativeData.GetAlternative();
            alternatives.Add(alternative);
        }
    }

    public Question GetQuestion()
    {
        return this.ThisQuestion;
    }
}
