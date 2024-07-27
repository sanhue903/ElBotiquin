
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
    private const string apiUrl = "https://whale-app-idf72.ondigitalocean.app/api";
    private const string appId = "BOTIQI";
    private const string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJmcmVzaCI6ZmFsc2UsImlhdCI6MTcyMTUzNDkzMCwianRpIjoiYzk1MWUzMzItMTIzYy00OWMwLTgxYmItNjc5OGE2OTgxM2RkIiwidHlwZSI6ImFjY2VzcyIsInN1YiI6IkJPVElRSSIsIm5iZiI6MTcyMTUzNDkzMCwiY3NyZiI6ImMwMGQ4YjdlLTllYjEtNDM1Yy04ZTc4LTA4MzdkNzdhMjY2NSJ9.ebRMG-lT3Wumg5eEz4iuCrHRM8la740LLn9CN3RJFmw";
    private List<RequestHeader> header;
    private string baseUrl;
      
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

        baseUrl = $"{apiUrl}/apps/{appId}/students";
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
        Debug.Log($"Sending Scores to:\n {baseUrl}");
        StartCoroutine(RestWebClient.Instance.HttpPost(baseUrl, 
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
        var json = JsonConvert.SerializeObject(
            new {
                chapter = new {
                    id = chapterId,
                    scores = scores
            }});
        Debug.Log(json);
        
        Debug.Log($"Sending Scores to:\n {baseUrl}/{student.id}/scores");
        StartCoroutine(RestWebClient.Instance.HttpPost($"{baseUrl}/{student.id}/scores", 
            json, (r) => OnSendScoresCompleted(r), header));
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