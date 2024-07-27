using UnityEngine;
using TMPro;
using RestClient.Core.Singletons;

public class ErrorManager : Singleton<ErrorManager> 
{
    [SerializeField] private UnityEngine.GameObject errorPanel;
    [SerializeField] private TextMeshProUGUI errorText;

    public void ShowError(string error)
    {
        errorText.text = error;
        errorPanel.SetActive(true);
    }

    public void CloseError()
    {
        errorPanel.SetActive(false);
    }
}