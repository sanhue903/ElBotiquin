
using System.Collections.Generic;
using RestClient.Core;
using RestClient.Core.Models;
using RestClient.Core.Singletons;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using System;

public class APIManager : Singleton<APIManager>
{
    //Singleton
    private static bool isCreated = false;

    //Information
    //private string auleCode;

    //API Settings
    private const string apiUrl = "http://127.0.0.1:5000";
    private const string appId = "BOTIQI";
    private const string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJmcmVzaCI6ZmFsc2UsImlhdCI6MTcyNTUwNDc4MywianRpIjoiNDZmNzEyMGEtZmZmYi00ZDkzLWFhMjktYTM2OWYxMzQ4ZDg2IiwidHlwZSI6ImFjY2VzcyIsInN1YiI6IkJPVElRSSIsIm5iZiI6MTcyNTUwNDc4MywiY3NyZiI6ImFhY2Q0M2IzLTdkMTAtNDgwMC1iNDQyLTA4YzYzODAyZGMwMCJ9.Sdfxn_qIAU42sO5k58nPfINxhkuset6vVeiQPOGU_A8";
    private List<RequestHeader> header;

    private bool isOnline = false;
      
    void Awake()
    {
        if (!isCreated)
        {
            DontDestroyOnLoad(gameObject);
            isCreated = true;
            
            this.header = new List<RequestHeader>();
            this.header.Add( new RequestHeader(Key: "Content-Type", Value: "application/json"));
            this.header.Add(new RequestHeader(Key: "Authorization", Value: "Bearer " + token));

            Debug.Log($"URL: {apiUrl}\nHeader: {this.header[0].Key} {this.header[0].Value}\n{this.header[1].Key} {this.header[1].Value}");
        }
        else
        {
            Debug.Log("Destroying APIManager");
            Destroy(gameObject);
        }

    }
    void Start()
    {
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

    private bool checkUrl(string url)
    {
        if (Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult))
        {
            return true;
        }
        
        Debug.LogError("Invalid URL: " + url);
        return false;
    }

    public void CreateStudentProfile(int age, string name)
    {
        if (!isOnline)
        {
            LoginManager.Instance.SaveStudentProfile(-1, age, name);
            return;
        }

        Debug.Log("Creating Student Profile");

        var json = JsonConvert.SerializeObject(new {age = age, name = name});

        string url = $"{apiUrl}/apps/{appId}/students"; 
        Debug.Log(json);
        Debug.Log($"Send student to:\n {url}");
        Debug.Log("Header: token " + this.header[1].Value);

        if (!checkUrl(url))
        {
            return;
        }

        Debug.Log("Sending request");
        StartCoroutine(RestWebClient.Instance.HttpPost(url, 
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
        if (!isOnline)
        {
            return;
        }

        Student student = LoginManager.Instance.actualStudent;
        var json = JsonConvert.SerializeObject(
            new {
                chapter = new {
                    id = chapterId,
                    scores = scores
            }});
        Debug.Log(json);
        
        string url = $"{apiUrl}/apps/{appId}/students/{student.id}/scores";

        Debug.Log($"Sending Scores to:\n {url}");

        if (!checkUrl(url))
        {
            return;
        }

        StartCoroutine(RestWebClient.Instance.HttpPost(url, 
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