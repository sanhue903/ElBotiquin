using UnityEngine;
using System;

public class DeleteButton : MonoBehaviour
{
    [SerializeField] UnityEngine.GameObject confirmationPanel;
    public void TryDelete()
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
        LoginManager.Instance.DeleteStudentProfile();
        confirmationPanel.SetActive(false);
}
}