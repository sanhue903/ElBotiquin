using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileButton : MonoBehaviour
{
    [SerializeField] UnityEngine.GameObject profileCreation;
    private Button profileButton;
    private TextMeshProUGUI profileButtonText;
    private void Start()
    {
        profileButton = GetComponent<Button>();
        profileButtonText = GetComponentInChildren<TextMeshProUGUI>();

        if (LoginManager.Instance.actualStudent == null)
        {
            profileButtonText.text = "Crear Perfil";
            profileButton.onClick.AddListener(() => profileCreation.SetActive(true));
            return;
        }

        profileButtonText.text = LoginManager.Instance.actualStudent.name;
        profileButton.onClick.AddListener(() => SceneManager.Instance.LoadScene("MainMenu"));
    }
}