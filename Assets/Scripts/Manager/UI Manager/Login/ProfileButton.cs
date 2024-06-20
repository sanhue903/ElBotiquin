using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileButton : MonoBehaviour
{
    [SerializeField] GameObject profileCreation;
    private Button profileButton;
    private TextMeshProUGUI profileButtonText;
    private void Start()
    {
        profileButton = GetComponent<Button>();
        profileButtonText = GetComponentInChildren<TextMeshProUGUI>();

        if (!SaveSystem.Check())
        {
            profileButtonText.text = "Crear Perfil";
            profileButton.onClick.AddListener(() => profileCreation.SetActive(true));

            return;
        }

        Student student = SaveSystem.Load();
        profileButtonText.text = student.name;
    
        profileButton.onClick.AddListener(() => LoginManager.Instance.LoadProfile(student));
    }
}