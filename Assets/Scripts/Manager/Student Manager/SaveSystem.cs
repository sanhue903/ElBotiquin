using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public static class SaveSystem
{
    private static readonly string SAVE_FOLDER = Application.persistentDataPath + "/Saves/";
    private static readonly string path = SAVE_FOLDER + "profile.json";
    public static int offlineSlot = 0;
    
    public static Student Create(int id = 0, int age = 0, string name = "#####")
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
        
        Student createdStudent = new Student(id:id, age:age, name:name);

        var json = JsonConvert.SerializeObject(createdStudent);
        Debug.Log(json);
        File.WriteAllText(path, json);

        return createdStudent;
    }

    public static void Save(int lastChapter)
    {
        
        Student saveStudent = Load();

        saveStudent.lastCompletedChapter = lastChapter;

        File.WriteAllText(path, JsonConvert.SerializeObject(saveStudent));
    }

    public static Student Load()
    {

        Student student = JsonConvert.DeserializeObject<Student>(File.ReadAllText(path));   
        APIManager.Instance.actualStudent = student;

        return student;
    }

    public static void Delete()
    {
        File.Delete(path);
    }

    public static bool Check()
    {
        return File.Exists(path);
    }
}

public enum EOnlineProfiles
{
        profile1 = 1,
        profile2 = 2,
        profile3 = 3
}


