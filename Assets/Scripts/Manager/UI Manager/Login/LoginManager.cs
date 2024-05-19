
using RestClient.Core.Singletons;
using UnityEngine;
using System;
using TMPro;

public class LoginManager : Singleton<LoginManager>
{
    public static int lastChapter;
    public static bool online;


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
                lastChapter = 1;
                online = true;
                break;
            
            case 404: 
                AuleCodeManager.Instance.ShowCodeMenu();
                break;

            default:
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