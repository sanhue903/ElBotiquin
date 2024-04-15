using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

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
        button = GetComponent<Button>();
        button.interactable = false;
    }

    void Update()
    {
        if (ageForm.text.Length == 0)
        {
            button.interactable = false;
            return;
        }
        
        button.interactable = true;
    }

    void OnEnable()
    {
        if (button == null)
        {
            button = GetComponent<Button>();
        }
        
        int age = int.Parse(ageForm.text);
        string name = nameForm.text;
        name = name.Replace("\u200B", string.Empty);


        button.onClick.AddListener(() => LoginManager.Instance.CreateStudentProfile(slot, age, name));
    }

}
