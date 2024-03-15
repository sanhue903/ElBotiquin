using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static readonly string SAVE_FOLDER = Application.persistentDataPath + "/Saves/";

    
    public static void Save(string jsonData, EProfiles slot)
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }

        string path = SAVE_FOLDER + "profile" + slot.ToString() + ".json";


        File.WriteAllText(path, jsonData);
    }

    public static Student Load(EProfiles slot)
    {
        string path = SAVE_FOLDER + "profile" + slot.ToString() + ".json";

        if (!File.Exists(path))
        {
            Save(JsonUtility.ToJson(new Student()), slot);
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