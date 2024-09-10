using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileButton : MonoBehaviour
{
    [SerializeField] UnityEngine.GameObject profileCreation;
    [SerializeField] GameObject deleteButton;
    private Button profileButton;
    private TextMeshProUGUI profileButtonText;
    private void Start()
    {
        profileButton = GetComponent<Button>();
        profileButtonText = GetComponentInChildren<TextMeshProUGUI>();

        if (LoginManager.Instance.actualStudent == null)
        {
            CreateProfile();

            return;
        }

        LoadProfile();
    }
    void Update()
    {

        if (LoginManager.Instance.actualStudent == null)
        {

            profileButton.onClick.RemoveAllListeners();
            CreateProfile();

            return;
        }
    
    }

    private void CreateProfile()
    {
        profileButtonText.text = "Crear Perfil";
        profileButton.onClick.AddListener(() => profileCreation.SetActive(true));
        deleteButton.SetActive(false);
    }

    private void LoadProfile()
    {
        profileButtonText.text = LoginManager.Instance.actualStudent.name;
        profileButton.onClick.AddListener(() => SceneManager.Instance.LoadScene("MainMenu"));
        deleteButton.SetActive(true);
    }
}