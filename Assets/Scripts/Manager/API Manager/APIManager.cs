
using System.Collections.Generic;
using RestClient.Core;
using RestClient.Core.Models;
using RestClient.Core.Singletons;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class APIManager : Singleton<APIManager>
{
    private RequestHeader header;
    private string auleCode;
    private string appMobileId;
    private int actualIdStudent;
    void Start()
    {
        header = new RequestHeader {
            Key = "Content-Type",
            Value = "application/json"
        };  
        appMobileId = "BOTIKI";
    }
    
    public void AuthAuleCode(string auleCode)
    {
        StartCoroutine(RestWebClient.Instance.HttpGet($"{RestWebClient.baseUrl}/apps/BOTIKI/aules/{auleCode}",
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

    public void CreateStudentProfile(int slot, int age, string name)
    {
        Student student = new Student();

        var serialiazedStudent = student.SerializeStudent();
        
        serialiazedStudent["name"] = name;
        serialiazedStudent["age"] = age.ToString(); 
        
        var studentJson = JsonConvert.SerializeObject(serialiazedStudent);
        
        StartCoroutine(RestWebClient.Instance.HttpPost($"{RestWebClient.baseUrl}/apps/BOTIKI/aules/{auleCode}/students", 
            studentJson, (r) => OnCreateStudentProfileCompleted(r, slot), new List<RequestHeader> { header } ));
    }

    private void OnCreateStudentProfileCompleted(Response response, int slot)
    {
        int statusCode = (int)response.StatusCode;
        if (statusCode == 201)
        {
            JObject data = JObject.Parse(response.Data);

            int id = (int)data["student"]["id"];   
            string name = (string)data["student"]["name"];
            int age = (int)data["student"]["age"];


            SaveSystem.Create(slot, id:id, age:age, name:name);
        }

        LoginManager.Instance.ManageProfileMenu(statusCode);
    }

    public void SetIdStudent(int id)
    {
        actualIdStudent = id;
    }

    public void SendScores(string chapterId, List<Score> scores)
    {
        //TODO serializar scores
        var scoresJson = JsonConvert.SerializeObject(scores);
        var Json = JsonConvert.SerializeObject(
            new {student_id = actualIdStudent, 
                    app_mobile = new {id = appMobileId,
                        chapter = new {id = chapterId,
                            scores = scoresJson
                     }}});
        Debug.Log(Json);
        
        StartCoroutine(RestWebClient.Instance.HttpPost($"{RestWebClient.baseUrl}/apps/BOTIKI/aules/{auleCode}/students/{actualIdStudent}/scores", 
            Json, (r) => OnSendScoresCompleted(r), new List<RequestHeader> { header } ));
    }

    private void OnSendScoresCompleted(Response response)
    {
        int statusCode = (int)response.StatusCode;
        ScoreManager.Instance.ManageScoreMenu(statusCode);
    }
    
}