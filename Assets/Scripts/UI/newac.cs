using UnityEngine;
using UnityEngine.UI;
using RestClient.Core.Singleton;
using System.Collections.Generic;

public class newac : newac
{
    [Header("UI Elements")]
    [SerializeField] private GameObject initParent;
    [SerializeField] private GameObject questionsParent;
    [SerializeField] private Button nextButton;
    [Header("Audio Settings")]
    [SerializeField] private float offsetTime = 0.5f;
    [Header("Information")]
    [SerializeField] private string chapterId;
    
}