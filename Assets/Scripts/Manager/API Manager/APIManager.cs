
using System.Collections.Generic;
using RestClient.Core;
using RestClient.Core.Models;
using RestClient.Core.Singletons;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class APIManager : Singleton<APIManager>
{
    private RequestHeader header;
    private string auleCode;


    void Start()
    {
        header = new RequestHeader {
            Key = "Content-Type",
            Value = "application/json"
        };  

        auleCode = "";
    }
    
    public void AuthAuleCode(string auleCode)
    {
        StartCoroutine(RestWebClient.Instance.HttpGet($"{RestWebClient.baseUrl}/apps/BOTIKI/aules/{auleCode}", (r) => OnAuthAuleCodeCompleted(r, auleCode)));
    }

    public void OnAuthAuleCodeCompleted(Response response, string  auleCode)
    {
        int statusCode = (int)response.StatusCode;

        if (statusCode == 200)                                                                                                          
        {
            this.auleCode = auleCode;
        }

        LoginManager.Instance.ManageCodeMenu(statusCode);
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

    public void OnCreateStudentProfileCompleted(Response response, int slot)
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

    
}