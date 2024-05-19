using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public static class SaveSystem
{
    private static readonly string SAVE_FOLDER = Application.persistentDataPath + "/Saves/";
    public static int offlineSlot = 0;
    
    public static void Create(int slot, int id = 0, int age = 0, string name = "#####")
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
        
        string path = SAVE_FOLDER + "profile" + slot.ToString() + ".json";
        Student createdStudent = new Student(id:id, age:age, name:name);
        APIManager.Instance.SetIdStudent(createdStudent.id);

        var json = JsonConvert.SerializeObject(createdStudent);
        Debug.Log(json);
        File.WriteAllText(path, json);
    }

    public static void Save(int slot, int lastChapter)
    {
        
        Student saveStudent = Load(slot);

        saveStudent.lastCompletedChapter = lastChapter;

        string path = SAVE_FOLDER + "profile" + slot.ToString() + ".json";
        File.WriteAllText(path, JsonConvert.SerializeObject(saveStudent));
    }

    public static Student Load(int slot)
    {
        string path = SAVE_FOLDER + "profile" + slot.ToString() + ".json";

        if (!File.Exists(path))
        {
            Create(slot);
        }

        Student student = JsonConvert.DeserializeObject<Student>(File.ReadAllText(path));   
        APIManager.Instance.SetIdStudent(student.id);

        return student;
    }
}

public enum EOnlineProfiles
{
        profile1 = 1,
        profile2 = 2,
        profile3 = 3
}


