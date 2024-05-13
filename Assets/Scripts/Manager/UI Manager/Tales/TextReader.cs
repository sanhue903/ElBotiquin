using System.Collections;
using UnityEngine;
using Newtonsoft.Json.Linq;
using TMPro;
public class TextReader : MonoBehaviour
{
    private float delay = 0.065f;
    [SerializeField] private TextMeshProUGUI textContainer;
    [SerializeField] private int indexParagraph;

    void Start()
    {
        StartCoroutine(Delay());
    }

    IEnumerator ReadText()
    {
        string fullText = TextManager.Instance.GetParagraph(indexParagraph);

        textContainer.text = "";

        foreach (char caracter in fullText)
        {
            textContainer.text += caracter;
            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator Delay()
    {
        if (indexParagraph == 0)
        {
            yield return new WaitForSeconds(.4f);
        }

        StartCoroutine(ReadText());
    }    
}
