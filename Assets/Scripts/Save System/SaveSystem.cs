using System.IO;
using System;
using UnityEngine;

public static class SaveSystem
{
    private static readonly string SAVE_FOLDER = Application.persistentDataPath + "/Saves/";

    
    public static Student Save(int slot, int id = 0,int age = 0, string name = "#####")
    {
        if (!Enum.IsDefined(typeof(EProfiles), slot))
        {
            Debug.LogError("Invalid slot");
            return null;
        }

        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }

        string path = SAVE_FOLDER + "profile" + slot.ToString() + ".json";
        Student saved_student = new Student(id:id, age:age, name:name);

        File.WriteAllText(path, JsonUtility.ToJson(saved_student));

        return saved_student;
    }

    public static Student Load(int slot)
    {
        if (!Enum.IsDefined(typeof(EProfiles), slot))
        {
            Debug.LogError("Invalid slot");
            return null;
        }

        string path = SAVE_FOLDER + "profile" + slot.ToString() + ".json";

        if (!File.Exists(path))
        {
            Save(slot);
        }

        return JsonUtility.FromJson<Student>(File.ReadAllText(path));
         
    }
}

public enum EProfiles
    {
        profile1,
        profile2,
        profile3
    }