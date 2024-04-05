using System.IO;
using System;
using UnityEngine;

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
        Student created_student = new Student(id:id, age:age, name:name);

        File.WriteAllText(path, JsonUtility.ToJson(created_student));
    }

    public static void Save(int slot, int lastChapter)
    {
        
        Student student = Load(slot);

        student.lastCompletedChapter = lastChapter;

        string path = SAVE_FOLDER + "profile" + slot.ToString() + ".json";
        File.WriteAllText(path, JsonUtility.ToJson(student));
    }

    public static Student Load(int slot)
    {
        string path = SAVE_FOLDER + "profile" + slot.ToString() + ".json";

        if (!File.Exists(path))
        {
            Create(slot);
        }

        return JsonUtility.FromJson<Student>(File.ReadAllText(path));
         
    }
}

public enum EOnlineProfiles
{
        profile1 = 1,
        profile2 = 2,
        profile3 = 3
}


