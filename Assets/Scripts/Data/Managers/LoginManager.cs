using RestClient.Core.Singletons;
using UnityEngine;

public class LoginManager : Singleton<LoginManager>
{
    //Singleton
    private static bool isCreated;

    //API Settings
    public static bool online = true;
    public Student actualStudent;

    void Awake()
    {
        if (!isCreated)
        {
            DontDestroyOnLoad(gameObject);
            isCreated = true;
            if(!SaveSystem.Check())
            {
                return;
            }

        actualStudent = SaveSystem.Load();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }
    public void CreateStudentProfile(int age, string name)
    {
        APIManager.Instance.CreateStudentProfile(age, name);
    }

    public void SaveStudentProfile(int id, int age, string name)
    {
        actualStudent = SaveSystem.Create(id, age, name);
        Debug.Log("Profile Created");
        SceneManager.Instance.LoadScene("MainMenu");
    }

    public void DeleteStudentProfile()
    {
        SaveSystem.Delete();
        actualStudent = null;
    }
}