
using RestClient.Core.Singletons;

public class LoginManager : Singleton<LoginManager>
{
    //Singleton
    private static bool isCreated;

    //API Settings
    public static bool online;
    public Student actualStudent;

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

    void Start()
    {
        if(!SaveSystem.Check())
        {
            actualStudent = null;
            return;
        }

        SaveSystem.Load();
    }
    public void CreateStudentProfile(int age, string name)
    {
        APIManager.Instance.CreateStudentProfile(age, name);
    }

    public void SaveStudentProfile(int id, int age, string name)
    {
        actualStudent = SaveSystem.Create(id, age, name);
        SceneManager.Instance.LoadScene("MainMenu");
    }
}