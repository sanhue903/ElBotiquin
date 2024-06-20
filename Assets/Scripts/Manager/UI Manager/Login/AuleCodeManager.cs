using RestClient.Core.Singletons;
using TMPro;
using UnityEngine;

public class AuleCodeManager : Singleton<AuleCodeManager>
{
    void Start()
    {
        Debug.Log("AuleCodeManager started");
    }
    /*
    [SerializeField] private GameObject codeMenu;  
    [SerializeField] private TextMeshProUGUI auleErrorText;
    public void SendCode(TextMeshProUGUI text)
    {
        string code = text.text;
        code = code.Replace("\u200B", string.Empty);

        APIManager.Instance.AuthAuleCode(code);
    }

    public void ShowCodeMenu()
    {
        codeMenu.SetActive(true);
    }
    public void ManageCodeMenu(int statusCode)
    {
        Debug.Log(statusCode);
        switch (statusCode)
        {
            case 200:
                codeMenu.SetActive(false);
                auleErrorText.text = "";
                break;
            
            case 404:
                auleErrorText.text = "Aula no encontrada";
                break;

            default:
                auleErrorText.text = "Error de conexión";
                break;
        }
    }*/
}