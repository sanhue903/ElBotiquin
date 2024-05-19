using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class ChangeParagraph : MonoBehaviour
{
    [SerializeField] private GameObject[] paragraphs; 
    [SerializeField] private string nextScene;

    public void ShowPreviousParagraph()
    {
        int actualParagraphIndex = 0;
        if (paragraphs.Length > 0)
        {

            for (int i = 0; i < paragraphs.Length; i++)
            {
               if (paragraphs[i].activeSelf)
               {
                   actualParagraphIndex = i;
                   break;
               }
            }
        }

        if (actualParagraphIndex > 0)
        {
            paragraphs[actualParagraphIndex].SetActive(false);
            paragraphs[actualParagraphIndex - 1].SetActive(true);
        }
    }

    public void ShowNextParagraph()
    {
        int actualParagraphIndex = 0;
        if (paragraphs.Length > 0)
        {

            for (int i = 0; i < paragraphs.Length; i++)
            {
                if (paragraphs[i].activeSelf)
                {
                    actualParagraphIndex = i;
                    break;
                }
            }
        }

        if (actualParagraphIndex < paragraphs.Length - 2)
        {
            paragraphs[actualParagraphIndex].SetActive(false);
            paragraphs[actualParagraphIndex + 1].SetActive(true);
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
        }
    }
}