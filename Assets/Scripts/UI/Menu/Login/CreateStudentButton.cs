using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class CreateStudentButton : MonoBehaviour
{
    private Button button;

    private int slot;
    [SerializeField] private TMP_InputField nameForm;
    [SerializeField] private TMP_InputField ageForm;

    public void SetSlot(int slot)
    {
        this.slot = slot;
    }
    void Start()
    {
    }

    void Update()
    {
        if (button == null)
        {
            return;
        }

        if (ageForm.text.Length == 0)
        {
            button.interactable = false;
            return;
        }
        
        button.interactable = true;
    }

    void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }

    public void SetCreateButton()
    {
        if (button == null)
        {
            Debug.LogError("Button is null");
            button = GetComponent<Button>();
        }

        button.onClick.RemoveAllListeners();
        
        int age = int.Parse(ageForm.text);

        string name = nameForm.text;
        name = name.Replace("\u200B", string.Empty);

        Debug.Log("Button Setted");

        button.onClick.AddListener(() => LoginManager.Instance.CreateStudentProfile(age, name));
    }
}
