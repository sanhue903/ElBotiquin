using UnityEngine;
using System;

public class DeleteButton : MonoBehaviour
{
    [SerializeField] GameObject confirmationPanel;
    public void TryDeleteSave()
   {
        if (LoginManager.Instance.actualStudent == null)
        {
            Debug.LogError("No profile to delete");
            return;
        }

        confirmationPanel.SetActive(true);    
    }

    public void ConfirmDelete()
    {
        //Verlo utilizando login manager
        SaveSystem.Delete();
        confirmationPanel.SetActive(false);
    }
}