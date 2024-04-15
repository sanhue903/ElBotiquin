
using RestClient.Core.Singletons;
using UnityEngine;
using System;
using TMPro;

public class LoginManager : Singleton<LoginManager>
{
    [SerializeField] private GameObject codeMenu;
    private TextMeshProUGUI auleErrorText;
    public static int lastChapter;
    public static bool online;

    void Start()
    {
        auleErrorText = codeMenu.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
    }

    public void SendAuleCode(TextMeshProUGUI text)
    {
        string code = text.text;
        code = code.Replace("\u200B", string.Empty);

        APIManager.Instance.AuthAuleCode(code);
    }

    public void ManageCodeMenu(int statusCode)
    {
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
    }

    public void CreateStudentProfile(int slot, int age, string name)
    {
        if (!Enum.IsDefined(typeof(EOnlineProfiles), slot))
        {
            Debug.LogError("Invalid slot");
            return;
        }

        APIManager.Instance.CreateStudentProfile(slot, age, name);
    }

    public void ManageProfileMenu(int statusCode)
    {
        switch (statusCode)
        {
            case 201:
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
                break;
            
            case 404: 
                codeMenu.SetActive(true);
                break;

            default:
                auleErrorText.text = "Error de conexión";
                break;
        }
    }

    public void LoadProfile(int slot)
    {
        if (!Enum.IsDefined(typeof(EOnlineProfiles), slot) && slot != SaveSystem.offlineSlot)
        {
            Debug.LogError("Invalid slot");
            return;
        }

        Student student = SaveSystem.Load(slot);

        lastChapter = student.lastCompletedChapter;
        online = (slot == SaveSystem.offlineSlot) ? true : false;

        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}