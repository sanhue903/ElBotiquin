using System.Collections;
using System.Collections.Generic;
using RestClient.Core.Singletons;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class TextManager : Singleton<TextManager>
{
    [SerializeField] private int numberTale;
    private JObject tale;

    void Awake()
    {
        LoadText(numberTale);        
    }
    private void LoadText(int numberTale)
    {
        tale = JObject.Parse(Resources.Load<TextAsset>("Tales/Tale" + numberTale.ToString()).ToString());
    }

    public string GetParagraph(int indexParagraph)
    {
        return tale["paragraphs"][indexParagraph].ToString();
    }
}
