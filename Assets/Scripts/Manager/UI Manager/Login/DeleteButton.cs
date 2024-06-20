using UnityEngine;
using System;

public class DeleteButton : MonoBehaviour
{
    [SerializeField] GameObject confirmationPanel;
    public void TryDeleteSave()
   {
        if (!SaveSystem.Check())
        {
            Debug.LogError("No profile to delete");
            return;
        }

        confirmationPanel.SetActive(true);    
        //SaveSystem.Delete();
    }

    public void ConfirmDelete()
    {
        SaveSystem.Delete();
        confirmationPanel.SetActive(false);
    }
}