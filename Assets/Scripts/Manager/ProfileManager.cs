using RestClient.Core;
using RestClient.Core.Models;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using RestClient.Core.Singletons;
using System.Data.Common;
using System.IO;
using System;

public class ProfileManager : Singleton<ProfileManager>
{
    List<Student> profiles;

    public void Awake()
    {
        profiles = new List<Student>();
        var EProfilesValues = Enum.GetValues(typeof(EProfiles));

        foreach (EProfiles slot in EProfilesValues)
        {
            profiles.Add(SaveSystem.Load(slot));
        }
    }

    public void CreateStudentProfile(TMP_InputField name, TMP_InputField age, EProfiles slot)
    {
        RequestHeader header = new RequestHeader {
            Key = "Content-Type",
            Value = "application/json"
        };  

        
        if (!Enum.IsDefined(typeof(EProfiles), slot))
        {
            Debug.LogError("Invalid slot");
            return;
        }
 
        Student student = profiles[(int)slot];

        Dictionary<string, string> serialiazedStudent = student.SerializeStudent();
        serialiazedStudent["name"] = name.text;
        serialiazedStudent["age"] = age.text; 
        //TODO dict to JSON
        StartCoroutine(RestWebClient.Instance.HttpPost($"{RestWebClient.baseUrl}/apps/BOTIKI/aules/{AuleManager.auleCode}/students", JsonUtility.ToJson(student), (r, slot) => OnStudentRequestComplete(r), new List<RequestHeader> { header } ));
    }

//TODO Cambiar todo
    private void OnStudentRequestComplete(Response response, int slot)
    {
        Debug.Log($"Status Code: {response.StatusCode}");

        if (response.StatusCode == 201)
        {
            //TODO sistema de slot de guardado(cambiar boton de crear a cargar)

            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
            return;
        }

        //TODO mensajes de error
    }     
}