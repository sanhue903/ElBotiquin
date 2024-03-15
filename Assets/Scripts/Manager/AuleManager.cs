using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RestClient.Core;
using RestClient.Core.Models;
using TMPro;
using RestClient.Core.Singletons;

public class AuleManager : Singleton<AuleManager>
{
    public static string auleCode {get; private set;}

    [SerializeField] private GameObject codeMenu;
    [SerializeField] private GameObject auleErrorText;

    public void VerifyAuleCode(TextMeshProUGUI text)
    {
        auleCode = text.text;
        auleCode = auleCode.Replace("\u200B", string.Empty);

        StartCoroutine(RestWebClient.Instance.HttpGet($"{RestWebClient.baseUrl}/apps/BOTIKI/aules/{auleCode}", (r) => OnAuleRequestComplete(r)));
    }

    private void OnAuleRequestComplete(Response response)
    {
        Debug.Log($"Status Code: {response.StatusCode}");

        if (response.StatusCode == 200)
        {
            codeMenu.SetActive(false);
            return;
        }

        
        if (response.StatusCode == 404)
        {
            auleErrorText.GetComponent<TextMeshProUGUI>().text = "Aula no encontrada";
            auleErrorText.SetActive(true);

            return;
        }

        auleErrorText.GetComponent<TextMeshProUGUI>().text = "Error de conexión";
        auleErrorText.SetActive(true);
    }
}
