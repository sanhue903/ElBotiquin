using System;    
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
        button = GetComponent<Button>();
        button.interactable = false;
    }

    void Update()
    {
        string name = nameForm.text;
        string age = ageForm.text;

        if (age.Length == 0 || name.Length == 0)
        {
            button.interactable = false;
            return;
        }
        //eliminar espacios en blanco
        name.Replace("\u200B", string.Empty);
        button.interactable = true;
    }

    void OnEnable()
    {
        button.onClick.AddListener(() => ProfileManager.Instance.CreateStudentProfile(slot, ageForm, nameForm));
    }
}
