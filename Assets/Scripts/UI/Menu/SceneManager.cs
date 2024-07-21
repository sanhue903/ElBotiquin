using RestClient.Core.Singletons;

public class SceneManager : Singleton<SceneManager>
{
    public void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
