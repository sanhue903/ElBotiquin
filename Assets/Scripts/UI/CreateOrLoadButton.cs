using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOrLoadButton : MonoBehaviour
{
    [SerializeField] private GameObject createProfile;
    [SerializeField] private GameObject loadProfile;

    public void SetCreateProfile()
    {
        createProfile.SetActive(true);
        loadProfile.SetActive(false);
    }

    public void SetLoadProfile()
    {
        createProfile.SetActive(false);
        loadProfile.SetActive(true);
    }
}
