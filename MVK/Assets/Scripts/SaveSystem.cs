using System.IO;
using UnityEngine;

public static class SaveSystem
{
    // Change to this one later
    // public static readonly string SAVE_FOLDER = Application.persistentDataPath + "/Saves/";
    private static readonly string SaveFolder = Application.dataPath + "/Saves/";
    
    // TODO: Let user input file name
    public static void SaveRoom(SceneData sceneData)
    {
        // Test to see if save folder exists
        if (!Directory.Exists(SaveFolder))
        {
            Directory.CreateDirectory(SaveFolder);
        }
        
        // Translate scene data to json format and write file
        var json = JsonUtility.ToJson(sceneData);
        File.WriteAllText(SaveFolder + sceneData.fileName + ".txt", json);
    }

    // TODO: Let user see all files and choose one.
    // TODO: Place all objects in scene.
    public static void LoadRoom()
    {
        // If file exists: open and read file
        if (File.Exists(SaveFolder + "room_name.txt")) {
            var saveString = File.ReadAllText(SaveFolder + "room_name.txt");
            Debug.Log("Loaded " + saveString);
            var sceneData = JsonUtility.FromJson<SceneData>(saveString);
        } 
        else
        {
            Debug.Log("Save file not found in " + SaveFolder + "room_name.txt" );
        }

    }
}
