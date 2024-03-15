using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ValidateNotNullForms : MonoBehaviour
{
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject nameForm;
    [SerializeField] private GameObject ageForm;

    void Update()
    {
        if (ageForm.GetComponent<TMP_InputField>().text == "" || nameForm.GetComponent<TMP_InputField>().text == "")
        {
            button.GetComponent<Button>().interactable = false;
            return;
        }
            //eliminar espacios en blanco
        nameForm.GetComponent<TMP_InputField>().text = nameForm.GetComponent<TMP_InputField>().text.Replace("\u200B", string.Empty);

            //habilitar boton
        button.GetComponent<Button>().interactable = true;
        
    }
}
