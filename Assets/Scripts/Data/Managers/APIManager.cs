
using System.Collections.Generic;
using RestClient.Core;
using RestClient.Core.Models;
using RestClient.Core.Singletons;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class APIManager : Singleton<APIManager>
{
    //Singleton
    private static bool isCreated;

    //Information
    //private string auleCode;

    //API Settings
    private const string baseUrl = "http://127.0.0.1:5000";
    private const string appId = "BOTIQI";
    private const string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJmcmVzaCI6ZmFsc2UsImlhdCI6MTcxODQ5MzU0NywianRpIjoiZWJmM2ZjOGYtZWJhOS00N2I4LWE4YTktNjIxYmZjOWVjODBmIiwidHlwZSI6ImFjY2VzcyIsInN1YiI6IkJPVElRSSIsIm5iZiI6MTcxODQ5MzU0NywiY3NyZiI6ImVlYWIzZGRhLTNiNWQtNDNiNC1hYmYyLTAwMjVmMWFlNzEyNSJ9.7ThEnvU-FdAEU-EASz-wGCoArKqy9r8ImSc8CIm-AGw";
    private List<RequestHeader> header;
      
    void Awake()
    {
        if (!isCreated)
        {
            DontDestroyOnLoad(gameObject);
            isCreated = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        header = new List<RequestHeader>();
        header.Add(new RequestHeader {
            Key = "Content-Type",
            Value = "application/json"
        });

        header.Add(new RequestHeader {
            Key = "Authorization",
            Value = "Bearer " + token
        });
    }
    
    // Aula no implementada en la api
    /*
    public void AuthAuleCode(string auleCode)
    {
        StartCoroutine(RestWebClient.Instance.HttpGet($"{RestWebClient.baseUrl}/apps/{appId}/aules/{auleCode}",
         (r) => OnAuthAuleCodeCompleted(r, auleCode)));
    }

    private void OnAuthAuleCodeCompleted(Response response, string auleCode)
    {
        int statusCode = (int)response.StatusCode;

        if (statusCode == 200)                                                                                                          
        {
            this.auleCode = auleCode;
        }

        AuleCodeManager.Instance.ManageCodeMenu(statusCode);
    }
    */

    public void CreateStudentProfile(int age, string name)
    {
        Debug.Log("Creating Student Profile");

        var json = JsonConvert.SerializeObject(new {age = age, name = name});

        Debug.Log(json);
        
        StartCoroutine(RestWebClient.Instance.HttpPost($"{baseUrl}/apps/{appId}/students", 
            json, (r) => OnCreateStudentProfileCompleted(r), header));
    }

    private void OnCreateStudentProfileCompleted(Response response)
    {
        Debug.Log("Status: " + response.StatusCode+"\n"
                  +"Data:"+ response.Data);

 
        switch (response.StatusCode)
        {
            case 201:
                JObject data = JObject.Parse(response.Data);

                int id = (int)data["student"]["id"];   
                string name = (string)data["student"]["name"];
                int age = (int)data["student"]["age"];

                LoginManager.Instance.SaveStudentProfile(id, age, name);
                break;
            
            default:
                Debug.LogError("Error creating profile");
                ErrorManager.Instance.ShowError($"Error al crear el perfil  estado: {response.StatusCode}");
                break;
        }
    }

    public void SendScores(string chapterId, List<Score> scores)
    {
        Student student = LoginManager.Instance.actualStudent;
        Debug.Log("Sending Scores");
        //TODO serializar scores
        var scoresJson = JsonConvert.SerializeObject(scores);
        var Json = JsonConvert.SerializeObject(
            new {student_id = student.id, 
                    app_mobile = new {id = appId,
                        chapter = new {id = chapterId,
                            scores = scoresJson
                     }}});
        Debug.Log(Json);
        
        StartCoroutine(RestWebClient.Instance.HttpPost($"{baseUrl}/apps/{appId}/students/{student.id}/scores", 
            Json, (r) => OnSendScoresCompleted(r), header));
    }

    private void OnSendScoresCompleted(Response response)
    {
        Debug.Log("Status: " + response.StatusCode+"\n"
                  +"Data:"+ response.Data);
    
        switch (response.StatusCode)
        {
            case 201:
                Debug.Log("Scores sent");
                break;
            
            default:
                Debug.LogError("Error sending scores");
                ErrorManager.Instance.ShowError($"Error al enviar las puntuaciones.  estado: {response.StatusCode}");
                break;
        }
    }
    
}