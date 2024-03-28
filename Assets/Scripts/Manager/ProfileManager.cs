using RestClient.Core;
using RestClient.Core.Models;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using RestClient.Core.Singletons;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
public class ProfileManager : Singleton<ProfileManager>
{
    public void Awake()
    {
        var EProfilesValues = Enum.GetValues(typeof(EProfiles));

        foreach (EProfiles slot in EProfilesValues)
        {
            SaveSystem.Load((int)slot);
        }
    }

    public void LoadStudentProfile(int slot)
    {
        Student student = SaveSystem.Load(slot);

        //TODO ver como cambiar los botones para crear y cargar estudiantes
        if (student.id == 0)
        {
            return;
        }
    }                                                                                       
    public void CreateStudentProfile(int slot, TMP_InputField age, TMP_InputField name)
    {
        if (!Enum.IsDefined(typeof(EProfiles), slot))
        {
            Debug.LogError("Invalid slot");
            return;
        }
 
        Student student = new Student();

        var serialiazedStudent = student.SerializeStudent();
        
        serialiazedStudent["name"] = name.text;
        serialiazedStudent["age"] = age.text; 
        
        var studentJson = JsonConvert.SerializeObject(serialiazedStudent);
        
        RequestHeader header = new RequestHeader {
            Key = "Content-Type",
            Value = "application/json"
        };  
        

        StartCoroutine(RestWebClient.Instance.HttpPost($"{RestWebClient.baseUrl}/apps/BOTIKI/aules/{AuleManager.auleCode}/students", 
            studentJson, (r) => OnStudentRequestComplete(r, slot), new List<RequestHeader> { header } ));
    }

    private void OnStudentRequestComplete(Response response, int slot)
    {
        Debug.Log($"Status Code: {response.StatusCode}");

        if (response.StatusCode == 201)
        {
            JObject data = JObject.Parse(response.Data);

            int id = (int)data["student"]["id"];   
            string name = (string)data["student"]["name"];
            int age = (int)data["student"]["age"];


            SaveSystem.Save(slot, id:id, age:age, name:name);

            
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
            return;
        }

        if (response.StatusCode == 404)
        {
            //auleErrorText.GetComponent<TextMeshProUGUI>().text = "Aula no encontrada";
            //auleErrorText.SetActive(true);

            return;
        }
    }     
}