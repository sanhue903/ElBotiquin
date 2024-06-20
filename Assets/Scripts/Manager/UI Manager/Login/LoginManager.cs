
using RestClient.Core.Singletons;
using UnityEngine;
using System;

public class LoginManager : Singleton<LoginManager>
{
    //Singleton
    private static bool isCreated;

    //API Settings
    public static bool online;

    void Awake()
    {
        if (!isCreated)
        {
            DontDestroyOnLoad(gameObject);
            isCreated = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void CreateStudentProfile(int age, string name)
    {
        APIManager.Instance.CreateStudentProfile(age, name);
    }

    public void ManageProfileMenu(int statusCode)
    {
        switch (statusCode)
        {
            case 201:
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
                online = true;
                break;
            
            default:
                Debug.LogError("Error creating profile");
                ErrorManager.Instance.ShowError($"Error al crear el perfil    {statusCode}");
                break;
        }
    }

    public void LoadProfile(Student student)
    {
        APIManager.Instance.actualStudent = student;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}