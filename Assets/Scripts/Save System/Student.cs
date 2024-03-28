using RestClient.Core;
using System.Collections.Generic;
public class Student
    {
        public int id {get; private set;}
        public int age {get; private set;}

         
        public string name {get; private set;}

                                                                                                                                              
        public Student(int id = 0, int age = 0, string name = "#####")
        {
            this.id       = id;
            this.age      = age;
            
            this.name     = name;
        }

        public Dictionary<string, string> SerializeStudent()
        {
            Dictionary<string, string> serializedStudent = new Dictionary<string, string>();

            serializedStudent.Add("age", this.age.ToString());
            serializedStudent.Add("name", this.name);

            return serializedStudent;
        }
    }