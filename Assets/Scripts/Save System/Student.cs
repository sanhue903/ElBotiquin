using RestClient.Core;
using System.Collections.Generic;
public class Student
    {
        public int id {get; private set;}
        public int age {get; private set;}

         
        public string name {get; private set;}
        public string mobileAppId {get; private set;}

                                                                                                                                              
        public Student()
        {
            this.id       = 0;
            this.age      = 0;
            
            this.name     = "#####";
            mobileAppId = RestWebClient.mobileAppId;
        }

        public Dictionary<string, string> SerializeStudent()
        {
            Dictionary<string, string> serializedStudent = new Dictionary<string, string>();

            serializedStudent.Add("id", this.id.ToString());
            serializedStudent.Add("age", this.age.ToString());
            serializedStudent.Add("name", this.name);
            serializedStudent.Add("mobile_app_id", this.mobileAppId);

            return serializedStudent;
        }
    }